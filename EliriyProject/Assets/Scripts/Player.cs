using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Location start_location = null;
    
    private const float SPEED_TRAVELING = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = start_location.transform.localPosition;
        startTravel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void startTravel()
    {
        Travel travel = start_location.getTravelInfo();

        StartCoroutine(coroutineTravel(travel));
    }

    private IEnumerator coroutineTravel( Travel travel )
    {
        Transform[] points = travel.way.getPoints();

        if ( travel.start_with_end )
            Array.Reverse(points);
        
        for ( int i = 0; i < points.Length - 1; i++ )
        {
            Vector2 first_point = points[i].position;
            Vector2 second_point = points[i + 1].position;

            float       distance       = Vector2.Distance(first_point, second_point);
            float       time_travel    = SPEED_TRAVELING / distance;
            float       percent        = 0.0f;
            const float PERCENT_FINISH = 1.0f;
            
            while (percent < PERCENT_FINISH)
            {
                Vector2 new_pos = Vector2.Lerp( first_point, second_point, percent );
                percent += time_travel * Time.deltaTime;
                
                setPlayerPos( new_pos );
                
                yield return null;
            }
        }
        
        void setPlayerPos( Vector2 pos )
        {
            transform.position = pos;
        }
    }
}
