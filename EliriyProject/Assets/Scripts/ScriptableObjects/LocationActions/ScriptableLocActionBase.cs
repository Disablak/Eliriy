using UnityEngine;


public class ScriptableLocActionBase : ScriptableObject
{
  [Header( "Need" )]
  public VarStory var_story_is_true = VarStory.NONE;
  public DayPart day_part = DayPart.NONE;
  
  [Header( "Main" )]
  public string name_action = string.Empty;
}

public enum VarStory
{
  NONE
}


