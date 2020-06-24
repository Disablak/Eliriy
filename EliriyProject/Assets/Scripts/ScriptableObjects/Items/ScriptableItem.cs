using UnityEngine;


[CreateAssetMenu( fileName = "Item", menuName = "Scriptable/Items/Item" )]
public class ScriptableItem : ScriptableObject
{
  [Header( nameof(ScriptableItem) )]
  public string item_name = string.Empty;
  public int price = 0;
}
