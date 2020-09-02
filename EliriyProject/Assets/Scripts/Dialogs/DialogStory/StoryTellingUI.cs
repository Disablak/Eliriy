using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using MEC;


public partial class StoryTellingUI : MonoBehaviourBase
{
  [SerializeField] private StoryUI   story_ui   = null;
  [SerializeField] private AnswersUI answers_ui = null;
  
  private Story story     = null;
  
  private bool canContinue => story_ui.storyCanContinue;
  private bool hasChoices => story.currentChoices.Count > 0;
  
  
  public void init( TextAsset text_asset )
  {
    answers_ui.init( tryToContinue );
    story_ui.init( tryToContinue );
    
    story = new Story( text_asset.text );
    
    //tryToContinue();
  }

  private void tryToContinue()
  {
    if ( story_ui.tryToSkipWritingText() )
      return;
    
    if ( !canContinue )
      return;

    if ( story.canContinue )
    {
      story_ui.createText( story.Continue(), canContinueWithText );
      return;
    }

    if ( hasChoices )
    {
      createAnswers();
      return;
    }

    closeDialog();

    void createAnswers() => story_ui.createAnswers( story, answers_ui, tryToContinue );

    void canContinueWithText()
    {
      if ( hasChoices )
        createAnswers();
    }
  }

  private void closeDialog()
  {
    return;
    
    Timing.RunCoroutine( delay() );
    
    IEnumerator<float> delay()
    {
      yield return Timing.WaitForSeconds( 0.2f );
      
      gameObject.SetActive( false );
      deleteAllElements();
    }
  }

  private void deleteAllElements()
  {
    // call in storyUI deleteAll method
  }
}
