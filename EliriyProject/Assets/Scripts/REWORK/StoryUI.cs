using System;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using MEC;
using UnityEngine;
using UnityEngine.UI;


public class StoryUI : MonoBehaviour
{
  [SerializeField] private Transform           content_story         = null;
  [SerializeField] private VerticalLayoutGroup vertical_layout_group = null;
  [SerializeField] private ContentSizeFitter   content_size_fitter   = null;
  [Header( "Elements" )]
  [SerializeField] private UIBlockText ui_block_text = null;
  [SerializeField] private UIBlockBanner ui_block_banner = null;

  private Action action_try_to_continue = null;
  private List<UIBlockText> texts_list = new List<UIBlockText>();
  
  private bool made_answer = true;
  private bool text_writing = false;

  public bool storyCanContinue => made_answer;
  
  private void updateLayouts()
  {
    Timing.RunCoroutine( tweenSymbols() );

    IEnumerator<float> tweenSymbols()
    {
      vertical_layout_group.enabled = false;
      content_size_fitter.enabled = false;
      yield return Timing.WaitForOneFrame;
      
      vertical_layout_group.enabled = true;
      content_size_fitter.enabled = true;
      vertical_layout_group.SetLayoutVertical();
    }
  }

  public void init( Action action_try_to_continue )
  {
    /*this.action_try_to_continue = action_try_to_continue;*/
  }

  public bool tryToSkipWritingText()
  {
    if ( texts_list.Count == 0 )
      return false;

    if ( !text_writing ) 
      return false;
    
    texts_list.Last().skip();
    return true;
  }
  
  public void createText( string text, Action can_continue_action )
  {
    UIBlockText new_block_text = Instantiate( ui_block_text, content_story );
    new_block_text.init( text.filter(), afterInitAction, updateLayouts );
    texts_list.Add( new_block_text );
    
    text_writing = true;

    void afterInitAction()
    {
      updateLayouts();
      can_continue_action?.Invoke();
      
      text_writing = false;
    }
  }
  
  public void createAnswers( Story story, AnswersUI answers_ui, Action create_text_action )
  {
    made_answer = false;
    
    List<Choice> choices = story.currentChoices;
    List<string> titles  = choices.Select( x => x.text ).ToList();
    List<Action> actions = choices.Select( choice => (Action) ( () => onClickAnswer( choice.index ) ) ).ToList();
    
    answers_ui.createAnswers( titles, actions/*, callbackTextDescription*/ );
    
    void onClickAnswer( int idx )
    {
      made_answer = true;
      text_writing = true;
      
      story.ChooseChoiceIndex( idx );
      create_text_action();
    }
    
    void callbackTextDescription( string text )
    {
      createText( text, create_text_action );
    }
  }
  
  public void createBanner( Sprite banner_sprite )
  {
    UIBlockBanner banner = Instantiate( ui_block_banner, content_story );
    banner.init( banner_sprite, updateLayouts );
    content_size_fitter.enabled = false;
  }
}
