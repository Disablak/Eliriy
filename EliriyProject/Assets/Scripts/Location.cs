using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Location : MonoBehaviourBase
{
    [Header( "Main" )]
    [SerializeField] private ScriptableLocation scriptable_location = null;
    [SerializeField] private List<Travel> travels = null;
    [Header( "Components" )]
    [SerializeField] private Clickable clickable = null;
    [SerializeField] private TextMeshPro txt_loc_name = null;
    [SerializeField] private SpriteRenderer sr_location = null;

    public ScriptableLocation getScriptableLocation => scriptable_location;
    public List<Travel> getTravels => travels;
    
    private void Awake()
    {
        clickable.onClick += onClick;
        
        foreach ( var travel in travels )
            travel.setStartLocation( this );

        txt_loc_name.text = scriptable_location.location_name;
    }
    
    private void onClick()
    {
        GameManager.instance.showDialogLocation( this );
    }

    protected override void initMyComponents()
    {
        base.initMyComponents();

        setLocationState( LocationState.NO_ANYBODY );
        
        foreach ( Travel travel in travels )
            travel.way.init();
    }

    public void init()
    {
        initComponents();
    }

    public void setLocationState( LocationState location_state )
    {
        Color player_here_color = new Color( 0.23f, 1f, 0f, 0.35f );
        Color no_anybody_color = new Color( 0.24f, 0.18f, 1f, 0.36f );
        
        switch ( location_state )
        {
            case LocationState.PLAYER_HERE:
                sr_location.color = player_here_color;
                break;
            
            case LocationState.NO_ANYBODY:
                sr_location.color = no_anybody_color;
                break;
            
            default:
                throw new ArgumentOutOfRangeException( nameof(location_state), location_state, null );
        }
    }
}

public enum LocationState
{
    PLAYER_HERE,
    NO_ANYBODY
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
