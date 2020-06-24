using UnityEngine;


[CreateAssetMenu( fileName = "Skill", menuName = "Scriptable/Hero/Skill" )]
public class ScriptableHeroSkill : ScriptableObject
{
  public string skill_name = string.Empty;
  public SkillTarget skill_target = SkillTarget.NONE;
  public bool use_weapon = false;
  [Space] 
  [Header("If skill dont use weapon")]
  public int damage = 0;
  public int heal = 0;
  public float chance_hit = 0.5f;
  public int cooldown = 0;
}
