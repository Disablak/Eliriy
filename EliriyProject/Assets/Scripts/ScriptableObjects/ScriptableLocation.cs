using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "Location", menuName = "Scriptable/Location" )]
public class ScriptableLocation : ScriptableObject
{
  public string location_name = string.Empty;
  public Sprite location_banner = null;
  [TextArea]
  public string location_description = string.Empty;
  [Space]
  public List<ScriptableLocationAction> location_actions = null;
}
