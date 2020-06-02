using UnityEngine;


[CreateAssetMenu( fileName = "LocationAction", menuName = "Scriptable/LocationAction" )]
public class ScriptableLocationAction : ScriptableObject
{
  [Header( "Need" )]
  public VarStory var_story_is_true = VarStory.NONE;
  public DayPart day_part = DayPart.NONE;
  
  [Header("Story")]
  public string name_story = string.Empty;
  public TextAsset text_story = null;
}

public enum VarStory
{
  NONE
}


