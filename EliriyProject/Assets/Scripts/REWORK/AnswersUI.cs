using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswersUI : UIBlockBase
{
  [SerializeField] private Clickable clickable_zone = null;
  [SerializeField] private UIAnswerButton ui_answer_button = null;
  [SerializeField] private GameObject go_scroll = null;
  [SerializeField] private Transform root_answers = null;
  [Header( "Layouts" )]
  [SerializeField] private VerticalLayoutGroup vertical_layout_group = null;
  [SerializeField] private ContentSizeFitter content_size_fitter = null;
  
  private List<UIAnswerButton> all_buttons = new List<UIAnswerButton>();
  private Action try_to_continue_action = null;

  protected override void initMyComponents()
  {
    clickable_zone.onClick += try_to_continue_action;
  }

  public void init( Action try_to_continue_action )
  {
    this.try_to_continue_action = try_to_continue_action;
    
    initComponents();
  }
  
  public void createAnswers( List<string> titles, List<Action> actions/*, Action<string> callback*/ )
  {
    canvas_group.alpha = 0.0f;
    
    for ( int i = 0; i < titles.Count; i++ )
    {
      int            index  = i;
      string title = titles[index].brackets();
      UIAnswerButton button = spawnButtonAnswer();
      button.init( title, () => onClick( actions[index], title ) );
    }

    void onClick( Action on_click, string text_title )
    {
      destroyAllButtons();
      
      //callback( text_title.bold() );
      
      on_click();
    }
    
    show();
  }

  private void updateLayouts()
  {
    vertical_layout_group.SetLayoutVertical();
    content_size_fitter.enabled = true;
  }

  private void destroyAllButtons()
  {
    all_buttons.ForEach( x => Destroy( x.gameObject ) );
    all_buttons = new List<UIAnswerButton>();
  }
  
  private UIAnswerButton spawnButtonAnswer( ButtonSize button_size = ButtonSize.LITTLE )
  {
    UIAnswerButton btn = Instantiate( ui_answer_button, root_answers );
    btn.rect_transform.sizeDelta = getBtnSize( button_size );
    all_buttons.Add( btn );
    return btn;
  }
  
  private Vector2 getBtnSize( ButtonSize button_size )
  {
    switch ( button_size )
    {
      case ButtonSize.LITTLE: return new Vector2( 980.0f, 90.0f );
      case ButtonSize.MEDIUM: return new Vector2( 980.0f, 150.0f );
      case ButtonSize.BIG:    return new Vector2( 980.0f, 200.0f );

      default:
        throw new ArgumentOutOfRangeException( nameof(button_size), button_size, null );
    }
  }
  
  protected override void setVisible()
  {
    base.setVisible();
    
    updateLayouts();
  }

    
  private enum ButtonSize
  {
    LITTLE,
    MEDIUM,
    BIG
  }
}
