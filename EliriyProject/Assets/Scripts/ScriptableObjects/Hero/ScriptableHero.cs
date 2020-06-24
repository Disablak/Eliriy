using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "Hero", menuName = "Scriptable/Hero/Hero" )]
public class ScriptableHero : ScriptableObject
{
  public string hero_name = string.Empty;
  [Space]
  public int strength     = 5;
  public int dexterity    = 5;
  public int intelligence = 5;
  [Header( "Weapon" )]
  public ScriptableItemWeapon weapon = null;
  [Header( "Equip" )]
  public ScriptableItemEquipable   equip  = null;
  [Header( "Skills" )]
  public List<ScriptableHeroSkill> skills = new List<ScriptableHeroSkill>();
}
