using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform start_location = null;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = start_location.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
