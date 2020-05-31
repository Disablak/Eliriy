using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] private Travel[] travels = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Travel getTravelInfo() => travels[0];
}

[Serializable]
public class Travel
{
    public Location target_location = null;
    public Way      way             = null;
    public bool     start_with_end  = false;
}
