using System;
using System.Collections.Generic;
using UnityEngine;


public class DialogsManager : MonoBehaviour
{
    [SerializeField] private StoryTellingUI storyTellingUi = null;
    [SerializeField] private DialogPlayer dialog_player = null;
    
    public static DialogsManager instance = null;

    
    private void Awake()
    {
        instance = this;

        GameEventManager.onPlayerEnterLocation += initDialogStory;
    }

    public void initDialogStory( Location location )
    {
        storyTellingUi.gameObject.SetActive( true );
    }
    
    public void initDialogStory( TextAsset test_asset, Action finish_action = null )
    {
        storyTellingUi.gameObject.SetActive( true );
        storyTellingUi.init( test_asset, finish_action );
    }

    public void initDialogBattle( List<Hero> my_heroes, List<Hero> enemies )
    {
        storyTellingUi.gameObject.SetActive( true );
        //dialog_story.initAsBattle( my_heroes, enemies, result => Debug.Log( $"Battle finished with result {result}" ) );
    }

    public void initDialogPlayer()
    {
        dialog_player.gameObject.SetActive( true );
        dialog_player.init();
    }
}
