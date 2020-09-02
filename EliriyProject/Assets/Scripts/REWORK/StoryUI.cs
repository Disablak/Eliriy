using System;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
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
  
  private bool maked_answer = true;

  public bool makedAnswer => maked_answer;
  
  private void updateLayouts()
  {
    vertical_layout_group.SetLayoutVertical();
    content_size_fitter.enabled = true;
  }

  public void createText( string text )
  {/*
    if ( text.Equals( string.Empty ) )
    {
      closeDialog();
      return;
    }
    */
    
    UIBlockText new_block_text = Instantiate( ui_block_text, content_story );
    new_block_text.init( text.filter(), updateLayouts );
    content_size_fitter.enabled = false;
  }
  
  public void createAnswers( Story story, AnswersUI answers_ui, Action try_to_continue_action )
  {
    maked_answer = false;
    
    List<Choice> choices = story.currentChoices;
    List<string> titles  = choices.Select( x => x.text ).ToList();
    List<Action> actions = choices.Select( choice => (Action) ( () => onClick( choice.index ) ) ).ToList();
    
    void onClick( int idx )
    {
      maked_answer = true;
      
      story.ChooseChoiceIndex( idx );
      try_to_continue_action();
    }

    answers_ui.createAnswers( titles, actions, createText );
  }
  
  public void createBanner( Sprite banner_sprite )
  {
    UIBlockBanner banner = Instantiate( ui_block_banner, content_story );
    banner.init( banner_sprite, updateLayouts );
    content_size_fitter.enabled = false;
  }
}
