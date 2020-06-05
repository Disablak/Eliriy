using UnityEngine;


public class DialogsManager : MonoBehaviour
{
    [SerializeField] private DialogStory dialog_story = null;
    
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
}
