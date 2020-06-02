﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManager
{
    #region Events
    #region Traveling
    public static event Action onPlayerStartTraveling  = () => {};
    public static event Action onPlayerFinishTraveling = () => {};
    public static event Action onPlayerCancelTraveling = () => {};
    #endregion
    
    public static event Action<Location> onPlayerEnterLocation = location => { };
    #endregion
    
    
    #region Invokes
    #region Traveling
    public static void invokePlayerStartTraveling()  => onPlayerStartTraveling();
    public static void invokePlayerFinishTraveling() => onPlayerFinishTraveling();
    public static void invokePlayerCancelTraveling() => onPlayerCancelTraveling();
    #endregion

    public static void invokePlayerEnterLocation( Location location )
    {
        Debug.Log($"invoke player enter location: {location.name}");
        onPlayerEnterLocation(location);
    }
    #endregion
}
