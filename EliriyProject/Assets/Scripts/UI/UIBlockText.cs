﻿using System;
using System.Collections.Generic;
using MEC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBlockText : UIBlockBase
{
  [SerializeField] private TextMeshProUGUI txt_block = null;
  [SerializeField] private ContentSizeFitter content_size_fitter = null;

  private const float SYMBOL_DELAY = 0.05f;
  
  private string future_text = string.Empty;

  private CoroutineHandle coroutine_tween_symbols = default;

  private Action update_global_layouts = null;
    
  public void init( string text, Action action_after_init, Action update_global_layouts )
  {
    this.action_after_init = action_after_init;
    //this.update_global_layouts = update_global_layouts;

    future_text = text;
    txt_block.text = string.Empty;
    
    show();
  }

  public void skip()
  {
    Timing.KillCoroutines( coroutine_tween_symbols );
    coroutine_tween_symbols = Timing.RunCoroutine( tweenSymbols() );

    IEnumerator<float> tweenSymbols()
    {
      txt_block.text = string.Empty;
      
      yield return Timing.WaitForOneFrame;
      
      txt_block.text = future_text;
      
      action_after_init();
    }
  }

  protected override void setVisible()
  {
    
    base.setVisible();

    coroutine_tween_symbols = Timing.RunCoroutine( tweenSymbols() );
    IEnumerator<float> tweenSymbols()
    {
      int counter = 0;

      while ( counter < future_text.Length )
      {
        txt_block.text += future_text[counter++];

        yield return Timing.WaitForSeconds( SYMBOL_DELAY );
      }
      
      action_after_init();
    }
  }
}
