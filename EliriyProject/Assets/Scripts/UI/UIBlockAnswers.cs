using System;
using System.Collections.Generic;
using UnityEngine;


public class UIBlockAnswers : UIBlockBase
{
  [SerializeField] private UIAnswerButton ui_answer_button = null;

  private List<UIAnswerButton> all_buttons = new List<UIAnswerButton>();
  
  
  public void init( List<string> titles, List<Action> actions, Action action_after_init )
  {
    this.action_after_init = action_after_init;
    
    for ( int i = 0; i < titles.Count; i++ )
    {
      int index = i;
      UIAnswerButton button = spawnButtonAnswer();
      button.init( titles[i], () => onClick( button, actions[index] ) );
    }

    void onClick( UIAnswerButton button, Action on_click )
    {
      on_click?.Invoke();
      button.markButton();
      all_buttons.ForEach( x => x.disableButton() );
    }
    
    show();
  }
  
  private UIAnswerButton spawnButtonAnswer( ButtonSize button_size = ButtonSize.LITTLE )
  {
    UIAnswerButton btn = Instantiate( ui_answer_button, transform );
    btn.rect_transform.sizeDelta = getBtnSize( button_size );
    all_buttons.Add( btn );
    return btn;
  }
  
  private Vector2 getBtnSize( ButtonSize button_size )
  {
    switch ( button_size )
    {
      case ButtonSize.LITTLE: return new Vector2( 900.0f, 90.0f );
      case ButtonSize.MEDIUM: return new Vector2( 900.0f, 150.0f );
      case ButtonSize.BIG:    return new Vector2( 900.0f, 200.0f );

      default:
        throw new ArgumentOutOfRangeException( nameof(button_size), button_size, null );
    }
  }
  
  private enum ButtonSize
  {
    LITTLE,
    MEDIUM,
    BIG
  }
}
