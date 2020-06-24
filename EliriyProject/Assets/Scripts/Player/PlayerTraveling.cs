using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerTraveling : MonoBehaviour
{
  #region Fields
  [SerializeField] private float speed_traveling = 0.5f;

  private Travel    current_travel   = null;
  private Coroutine travel_coroutine = null;

  private int cur_travel_point_idx = 0;
  private bool start_with_end = false;
  #endregion


  #region Public Methods
  public void startTravel( Travel travel )
  {
    current_travel = travel;
    
    travel_coroutine = StartCoroutine( coroutineTravel() );
  }
  
  public void cancelTraveling()
  {
    StopCoroutine( travel_coroutine );

    GameEventManager.invokePlayerCancelTraveling();
        
    travel_coroutine = StartCoroutine( cancelTraveling() );

    IEnumerator cancelTraveling()
    {
      int         start_idx = cur_travel_point_idx;
      Transform[] points    = current_travel.way.getPoints();

      for (int i = start_idx; i >= 0; i--)
      {
        Vector2 first_point  = transform.position;
        Vector2 second_point = points[i].position;
                
        yield return moveFromOneToAnother(first_point, second_point);
      }

      GameEventManager.invokePlayerEnterLocation( current_travel.start_location );
    }
  }
  
  public void pauseTravel()
  {
    StopCoroutine( travel_coroutine );
  }

  public void unpauseTravel()
  {
    int start_point = cur_travel_point_idx;

    Transform[] points = current_travel.way.getPoints().ToArray();
    if ( start_with_end )
      Array.Reverse( points );

    travel_coroutine = StartCoroutine( continueTraveling() );

    IEnumerator continueTraveling()
    {
      if ( start_with_end )
      {
        for ( int i = start_point; i >= 0; i-- )
        {
          Vector2 first_point  = transform.position;
          Vector2 second_point = points[i].position;
                
          yield return moveFromOneToAnother(first_point, second_point);
        }
      }
      else
      {
        for ( int i = start_point + 1; i < points.Length; i++ )
        {
          Vector2 first_point  = transform.position;
          Vector2 second_point = points[i].position;
                
          yield return moveFromOneToAnother(first_point, second_point);
        }
      }

      Location finish_loc = start_with_end ? current_travel.start_location : current_travel.target_location;
      GameEventManager.invokePlayerEnterLocation( finish_loc );
    }
  }
  #endregion

  #region Coroutines
  private IEnumerator coroutineTravel()
  {
    GameEventManager.invokePlayerStartTraveling();
        
    Transform[] points = current_travel.way.getPoints();
    ScriptableWayEvent way_event = current_travel.way.way_event;
    int event_point = points.Length / 2;
    start_with_end = current_travel.start_with_end;

    if ( start_with_end )
      Array.Reverse(points);
        
    for ( int i = 0; i < points.Length - 1; i++ )
    {
      tryToCallEvent( i, event_point, way_event );
      
      Vector2 first_point  = points[i].position;
      Vector2 second_point = points[i + 1].position;

      cur_travel_point_idx = i;

      yield return moveFromOneToAnother(first_point, second_point);
    }

    GameEventManager.invokePlayerFinishTraveling();
    GameEventManager.invokePlayerEnterLocation( current_travel.target_location );
  }

  private IEnumerator moveFromOneToAnother( Vector2 first_point, Vector2 second_point )
  {
    float       distance       = Vector2.Distance(first_point, second_point);
    float       time_travel    = speed_traveling / distance;
    float       percent        = 0.0f;
    const float PERCENT_FINISH = 1.0f;
            
    while (percent < PERCENT_FINISH)
    {
      Vector2 new_pos = Vector2.Lerp( first_point, second_point, percent );
      percent += time_travel * Time.deltaTime;
                
      setPlayerPos( new_pos );
                
      yield return null;
    }
        
    void setPlayerPos( Vector2 pos ) => transform.position = pos;
  }
  
  private void tryToCallEvent( int cur_point, int event_point, ScriptableWayEvent way_event )
  {
    if ( cur_point != event_point )
      return;

    if ( !way_event )
      return;

    /*if ( way_event.already_played )
      return;*/

    if ( Random.value <= way_event.chance )
    {
      Debug.Log( $"way event {way_event.story.name_action} ({way_event.name})" );
      //way_event.already_played = true;
      pauseTravel();
      DialogsManager.instance.initDialogStory( way_event.story.text_story, unpauseTravel );
    }
  }
  #endregion
}
