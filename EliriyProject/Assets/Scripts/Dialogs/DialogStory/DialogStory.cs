﻿using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogStory : MonoBehaviour
{
    [SerializeField] private GameObject tab_location = null;
    [SerializeField] private GameObject tab_story = null;
    [Space]
    [Header("TAB location")]
    [SerializeField] private TextMeshProUGUI txt_loc_name   = null;
    [SerializeField] private TextMeshProUGUI txt_loc_desc   = null;
    [SerializeField] private Image           img_loc_banner = null;
    
    [Header("TAB story")]
    [SerializeField] private TextMeshProUGUI txt_story_desc = null;
    
    [Header("Other")]
    [SerializeField] private Transform      root_answers     = null;
    [SerializeField] private UIAnswerButton ui_answer_button = null;


    private List<UIAnswerButton> all_buttons = new List<UIAnswerButton>();
    
    public void init( Location location )
    {
        changeTabs( true );

        ScriptableLocation scriptable_location = location.getScriptableLocation;
        txt_loc_name.text = scriptable_location.location_name;
        txt_loc_desc.text = scriptable_location.location_description;
        img_loc_banner.sprite = scriptable_location.location_banner;

        spawnActions( scriptable_location.location_actions, location.getTravels );
    }
    
    public void init( TextAsset text_asset )
    {
        changeTabs( false );

        Story story = new Story( text_asset.text );
        loadStoryText( story );
    }
    
    #region LocationPart
    private void spawnActions( List<ScriptableLocActionBase> location_actions, List<Travel> travels )
    {
        foreach ( ScriptableLocActionBase act in location_actions )
        {
            UIAnswerButton ui_answer_button = spawnButtonAnswer();
            ui_answer_button.init( act.name_action, makeAnswer );
            
            void makeAnswer()
            {
                destroyAllButtons();
                
                if ( act is ScriptableLocActionStory story )
                    init( story.text_story );

                if ( act is ScriptableLocActionTravels )
                    spawnActions( travels );
            }
        }
    }

    private void spawnActions( List<Travel> travels )
    {
        foreach ( Travel travel in travels )
        {
            UIAnswerButton ui_answer_button = spawnButtonAnswer();
            string loc_name = travel.target_location.getScriptableLocation.location_name;
            ui_answer_button.init( loc_name, makeAnswer );

            void makeAnswer()
            {
                destroyAllButtons();
                closeDialog();
                GameManager.instance.makeTravel( travel );
            }
        }
    }
    #endregion
    
    #region StoryPart
    private void loadStoryText( Story story )
    {
        if ( story.canContinue )
            txt_story_desc.text = story.ContinueMaximally();

        spawnAnswers( story );
    }

    private void spawnAnswers( Story story )
    {
        List<Choice> choices = story.currentChoices;

        if ( choices.Count == 0 )
        {
            UIAnswerButton answer_button = spawnButtonAnswer();
            answer_button.init( "Закончить", onClick );

            void onClick()
            {
                destroyAllButtons();
                closeDialog();
            }
        }

        for ( var i = 0; i < choices.Count; i++ )
        {
            Choice choice = choices[i];
            int    index  = i;

            UIAnswerButton answer_button = spawnButtonAnswer();
            answer_button.init( choice.text, makeAnswer );

            void makeAnswer()
            {
                destroyAllButtons();
                
                story.ChooseChoiceIndex( index );
                loadStoryText( story );
            }
        }
    }
    #endregion

    private UIAnswerButton spawnButtonAnswer()
    {
        UIAnswerButton btn = Instantiate( ui_answer_button, root_answers );
        all_buttons.Add( btn );
        return btn;
    }

    private void destroyAllButtons()
    {
        foreach ( var btn in all_buttons )
            Destroy( btn.gameObject );
        
        all_buttons = new List<UIAnswerButton>();
    }
    
    private void changeTabs( bool is_location )
    {
        tab_location.SetActive( is_location );
        tab_story.SetActive( !is_location );
    }
    
    private void closeDialog()
    {
        gameObject.SetActive( false );
    }
}
