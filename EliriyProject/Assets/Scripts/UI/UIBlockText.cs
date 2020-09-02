using System;
using System.Collections.Generic;
using MEC;
using TMPro;
using UnityEngine;


public class UIBlockText : UIBlockBase
{
  [SerializeField] private TextMeshProUGUI txt_block = null;

  private string future_text = string.Empty;

  public void init( string text, Action action_after_init )
  {
    this.action_after_init = action_after_init;

    future_text = text;
    txt_block.text = string.Empty;
    
    show();
  }

  protected override void setVisible()
  {
    base.setVisible();

    const float SYMBOL_DELAY = 0.05f;
    
    Timing.RunCoroutine( tweenSymbols() );

    IEnumerator<float> tweenSymbols()
    {
      int counter = 0;

      while ( counter < future_text.Length )
      {
        txt_block.text += future_text[counter++];

        yield return Timing.WaitForSeconds( SYMBOL_DELAY );
      }
    }
  }
}
