﻿using UnityEngine;


public class GameManager : MonoBehaviour
{
  [SerializeField] private Location start_location = null;
  [SerializeField] private Player player = null;
  [SerializeField] private DialogsManager dialogs_manager = null;
  [SerializeField] private LocationsManager locations_manager = null;
  [Header( "Info" )]
  [SerializeField] private DayPart cur_day_part = DayPart.DAY;

  private Location current_location = null;
  
  public static GameManager instance = null;
  
  
  private void Awake()
  {
    instance = this;

    GameEventManager.onPlayerEnterLocation += onPlayerEnterLocation;
    GameEventManager.onPlayerLeaveLocation += onPlayerLeaveLocation;
    
    locations_manager.init();
    
    makeTravel( start_location.getTravels[0] );
  }
  
  private void onPlayerEnterLocation( Location location )
  {
    current_location = location;
    current_location.setLocationState( LocationState.PLAYER_HERE );
  }

  private void onPlayerLeaveLocation( Location _ )
  {
    current_location = null;
  }

  public void makeTravel( Travel travel )
  {
    player.startTraveling( travel );
    travel.start_location.setLocationState( LocationState.NO_ANYBODY );
  }

  public void cancelTravel()
  {
    player.cancelTraveling();
  }

  public void showDialogLocation( Location location )
  {
    if ( current_location == null || !current_location.Equals( location ) )
    {
      Debug.Log( $"im not there!" );
      return;
    }
    
    dialogs_manager.initDialogStory( location );
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
