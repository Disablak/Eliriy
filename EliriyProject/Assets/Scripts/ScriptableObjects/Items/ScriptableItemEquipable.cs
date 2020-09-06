using UnityEngine;


[CreateAssetMenu( fileName = "Equipable", menuName = "Scriptable/Items/Equipable" )]
public class ScriptableItemEquipable : ScriptableItem
{
  [Header( nameof(ScriptableItemEquipable) )] 
  public float absorption = 0.1f;
  [Space] 
  public int bonus_strength     = 0;
  public int bonus_dexterity    = 0;
  public int bonus_intelligence = 0;
}
