using System.Collections.Generic;
using UnityEngine;


public class LocationsManager : MonoBehaviour
{
  private List<Location> all_locations = null;


  public void init()
  {
    all_locations = transform.getComponentAllChildrenList<Location>();

    foreach ( Location location in all_locations )
      location.init();
  }
}
