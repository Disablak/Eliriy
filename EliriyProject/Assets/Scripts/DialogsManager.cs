using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;


public class DialogsManager : MonoBehaviour
{
    [SerializeField] private DialogStory dialog_story = null;
    [SerializeField] private DialogPlayer dialog_player = null;
    
    public static DialogsManager instance = null;

    
    private void Awake()
    {
        instance = this;

        GameEventManager.onPlayerEnterLocation += initDialogStory;
    }

    public void initDialogStory( Location location )
    {
        dialog_story.gameObject.SetActive( true );
        dialog_story.init( location );
    }
    
    public void initDialogStory( TextAsset test_asset, Action finish_action = null )
    {
        dialog_story.gameObject.SetActive( true );
        dialog_story.init( test_asset, finish_action );
    }

    public void initDialogBattle( List<Hero> my_heroes, List<Hero> enemies )
    {
        dialog_story.gameObject.SetActive( true );
        dialog_story.initAsBattle( my_heroes, enemies );
    }

    public void initDialogPlayer()
    {
        dialog_player.gameObject.SetActive( true );
        dialog_player.init();
    }
}
