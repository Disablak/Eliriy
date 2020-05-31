using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManager
{
    public static event Action onPlayerStartTraveling = () => {};
    public static event Action onPlayerFinishTraveling = () => {};
    public static event Action onPlayerCancelTraveling = () => {};

    public static void invokePlayerStartTraveling() => onPlayerStartTraveling();
    public static void invokePlayerFinishTraveling() => onPlayerFinishTraveling();
    public static void invokePlayerCancelTraveling() => onPlayerCancelTraveling();
}
