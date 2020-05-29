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
    
    
    [Serializable]
    public class Travel
    {
        [SerializeField] private Location target_location = null;
        [SerializeField] private Way      way             = null;
    }
}
