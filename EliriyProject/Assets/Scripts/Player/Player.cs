﻿using UnityEngine;


public class Player : MonoBehaviour
{
    [Header( "Player Components" )]
    [SerializeField] private SpriteRenderer sprite_renderer = null;
    [SerializeField] private PlayerTraveling player_traveling = null;
    [SerializeField] private Hero hero = null;

    private void Awake()
    {
        GameEventManager.onPlayerStartTraveling  += onPlayerStartTravel;
        GameEventManager.onPlayerFinishTraveling += onPlayerFinishTravel;
    }

    public void startTraveling( Travel travel )
    {
        GameEventManager.invokePlayerLeaveLocation( travel.start_location );
        player_traveling.startTravel( travel );
    }

    public void cancelTraveling()
    {
        player_traveling.cancelTraveling();
    }

    private void onPlayerStartTravel()  => sprite_renderer.enabled = true;
    private void onPlayerFinishTravel() => sprite_renderer.enabled = false;
}
