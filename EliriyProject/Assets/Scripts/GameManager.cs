using UnityEngine;


public class GameManager : MonoBehaviour
{
  [SerializeField] private Location start_location = null;
  [SerializeField] private Player player = null;
  [SerializeField] private DialogsManager dialogs_manager = null;
  [Header( "Info" )]
  [SerializeField] private DayPart cur_day_part = DayPart.DAY;

  private Location current_location = null;
  
  public static GameManager instance = null;
  
  
  private void Awake()
  {
    instance = this;

    GameEventManager.onPlayerEnterLocation += onPlayerEnterLocation;
  }

  private void Start()
  {
    makeTravel( start_location.getTravels[0] );
  }

  private void onPlayerEnterLocation( Location location )
  {
    current_location = location;
  }

  public void makeTravel( Travel travel )
  {
    player.startTraveling( travel );
  }

  public void cancelTravel()
  {
    player.cancelTraveling();
  }

  public void showDialogLocation( Location location )
  {
    if ( current_location == null )
      return;
    
    if ( current_location.Equals( location ) )
      dialogs_manager.initDialogStory( location );
    else
      Debug.Log( $"im not there!" );
  }
}

public enum DayPart
{
  NONE,
  [InspectorName("Day")]
  DAY,
  [InspectorName("Red Night")]
  RED_NIGHT,
  [InspectorName("Black Night")]
  BLACK_NIGHT
}
