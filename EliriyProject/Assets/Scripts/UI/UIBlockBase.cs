using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;


public class UIBlockBase : MonoBehaviourBase
{
  [SerializeField] protected CanvasGroup canvas_group = null;
  
  protected Action action_after_init = null;
  
  protected virtual void setVisible()
  {
    const float TIME_TWEEN = 2.0f;
    
    Timing.RunCoroutine( tweenAlpha() );

    IEnumerator<float> tweenAlpha()
    {
      return IEnumeratorExtansion.tweenFloat( 0.0f, TIME_TWEEN, x => canvas_group.alpha = x / TIME_TWEEN );
    }
  }

  protected void show()
  {
    Timing.RunCoroutine( delay() );
    
    IEnumerator<float> delay()
    {
      yield return Timing.WaitForOneFrame;
      
      action_after_init?.Invoke();
      setVisible();
    }
  }
}
