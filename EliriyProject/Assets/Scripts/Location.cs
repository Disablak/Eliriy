using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Location : MonoBehaviour
{
    [SerializeField] private ScriptableLocation scriptable_location = null;
    [SerializeField] private TextMeshPro txt_loc_name = null;
    [SerializeField] private List<Travel> travels = null;

    public ScriptableLocation getScriptableLocation => scriptable_location;
    public List<Travel> getTravels => travels;
    
    private void Awake()
    {
        foreach ( var travel in travels )
            travel.setStartLocation( this );

        txt_loc_name.text = scriptable_location.location_name;
    }

    private void OnMouseUp()
    {
        GameManager.instance.showDialogLocation( this );
    }
}

[Serializable]
public class Travel
{
    public Location target_location = null;
    public Way      way             = null;
    public bool     start_with_end  = false;
    public Location start_location { get; private set; } = null;

    public void setStartLocation(Location location) => start_location = location;
}
