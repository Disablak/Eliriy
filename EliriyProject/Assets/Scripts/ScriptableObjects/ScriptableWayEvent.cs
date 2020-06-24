using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "WayEvent", menuName = "Scriptable/Way Event" )]
public class ScriptableWayEvent : ScriptableObject
{
  public ScriptableLocActionStory story;
  public float chance = 1.0f;
  //public bool already_played = false;
}
