using System;
using System.Collections;
using UnityEngine;


public class PlayerTraveling : MonoBehaviour
{
  #region Fields
  [SerializeField] private float speed_traveling = 0.5f;

  private Travel    current_travel   = null;
  private Coroutine travel_coroutine = null;

  private int cur_travel_point_idx = 0;
  #endregion

  
  public void startTravel( Travel travel )
  {
    current_travel = travel;
    
    travel_coroutine = StartCoroutine( coroutineTravel() );
  }
  
  public void reverseTraveling()
  {
    StopCoroutine(travel_coroutine);

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

  #region Coroutines
  private IEnumerator coroutineTravel()
  {
    GameEventManager.invokePlayerStartTraveling();
        
    Transform[] points = current_travel.way.getPoints();

    if ( current_travel.start_with_end )
      Array.Reverse(points);
        
    for ( int i = 0; i < points.Length - 1; i++ )
    {
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
  #endregion
}
