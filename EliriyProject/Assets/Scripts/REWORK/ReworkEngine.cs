using UnityEngine;


public class ReworkEngine : MonoBehaviour
{
  [SerializeField] private TextAsset main_story = null;
  [Space]
  [SerializeField] private StoryTellingUI story_telling_ui = null;
  
  
  private void Awake()
  {
    story_telling_ui.init( main_story );
  }
}
