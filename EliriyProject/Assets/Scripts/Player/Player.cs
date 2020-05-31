using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Location current_location = null;
    [Header( "Player Components" )]
    [SerializeField] private PlayerTraveling player_traveling = null;

    public static Player instance = null;

    public PlayerTraveling getPlayerTraveling => player_traveling;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player_traveling.startTravel( current_location.getTravelInfo() );
    }
}
