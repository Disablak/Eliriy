using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;


public static class IEnumeratorExtansion
{
  public static IEnumerator<float> tweenFloat( float time_from, float time_to, Action<float> invoke_action )
  {
    float total_time = Mathf.Abs( time_to - time_from );
    float cur_time = 0.0f;
      
    while ( cur_time < total_time )
    {
      cur_time += Time.deltaTime;
      invoke_action( Mathf.Lerp( time_from, time_to, cur_time / total_time ) );
        
      yield return Timing.WaitForOneFrame;
    }
  }

  public static float waitUntilDone( IEnumerator<float> coroutine )
  {
    return Timing.WaitUntilDone( Timing.RunCoroutine( coroutine ) );
  }
}
