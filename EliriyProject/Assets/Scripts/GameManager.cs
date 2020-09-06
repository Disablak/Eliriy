using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
  [SerializeField] private Location         start_location = null;
  [SerializeField] private DialogsManager   dialogs_manager = null;
  [SerializeField] private LocationsManager locations_manager = null;
  [Header( "Player" )]
  [SerializeField] private Player     player       = null;
  [SerializeField] private PlayerInfo player_info  = null;
  [SerializeField] private DayPart    cur_day_part = DayPart.DAY;
  [Header( "Test" )]
  public List<ScriptableHero> scr_hero = null;
  public List<ScriptableHero> scr_enemies = null;

  private Location current_location = null;
  public PlayerInfo PlayerInfo => player_info;
  
  public static GameManager instance = null;
  private List<Hero> my_squad = new List<Hero>();
  private List<Hero> enemy_squad = new List<Hero>();
  
  
  private void Awake()
  {
    instance = this;

    startSettings();
  }

  private void startSettings()
  {
    GameEventManager.onPlayerEnterLocation += onPlayerEnterLocation;
    GameEventManager.onPlayerLeaveLocation += onPlayerLeaveLocation;
    
    locations_manager.init();

    player_info.money = 150;

    //createHeroSquadAndStartBattle();
    
    makeTravel( start_location.getTravels[0] );
  }

  private void createHeroSquadAndStartBattle()
  {
    foreach ( ScriptableHero hero in scr_hero )
    {
      my_squad.Add( new Hero( hero ) );
    }

    foreach ( ScriptableHero enemy in scr_enemies )
    {
      enemy_squad.Add( new Hero( enemy ) );
    }

    DialogsManager.instance.initDialogBattle( my_squad, enemy_squad );
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
