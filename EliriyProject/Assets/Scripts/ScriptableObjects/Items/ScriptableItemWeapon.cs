using UnityEngine;


[CreateAssetMenu( fileName = "Weapon", menuName = "Scriptable/Items/Weapon" )]
public class ScriptableItemWeapon : ScriptableItem
{
  [Header( nameof(ScriptableItemWeapon) )] 
  public int damage = 0;
  public float critical_damage = 1.0f;
  public float armor_punch = 0.0f;
}
