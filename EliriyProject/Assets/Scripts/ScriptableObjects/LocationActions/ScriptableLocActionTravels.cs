using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "LocationAction", menuName = "Scriptable/LocActionTravels" )]
public class ScriptableLocActionTravels : ScriptableLocActionBase
{
  public List<string> location_target_names { get; } = null;
  public List<Travel> travels { get; private set; } = null;



  public void init( List<Travel> travels )
  {
    this.travels = travels;
    
    foreach ( Travel travel in travels )
    {
      string location_name = travel.target_location.getScriptableLocation.location_name;
      location_target_names.Add( location_name );
    }
  }
}
