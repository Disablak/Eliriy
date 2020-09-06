using System.Collections.Generic;
using UnityEngine;


public class PlayerInfo : MonoBehaviour
{
  [SerializeField] private List<ScriptableItem> start_items = null;
  [SerializeField] private Hero player_hero = null;
  
  public int Strength
  {
    get => strength;
    set => strength = value;
  }

  public int Dexterity
  {
    get => dexterity;
    set => dexterity = value;
  }

  public int Intelligence
  {
    get => intelligence;
    set => intelligence = value;
  }

  private int strength     = 0;
  private int dexterity    = 0;
  private int intelligence = 0;

  public int money = 0;

  public List<IKnow> i_know { get; } = new List<IKnow>();
  public List<ScriptableItem> my_items { get; } = new List<ScriptableItem>();

  public Hero getPlayer => player_hero;

  public bool iKnow( IKnow know )
  {
    return i_know.Contains( know );
  }

  public bool iHave() // TODO add enum
  {
    return false;
  }

  private void Awake()
  {
    foreach ( ScriptableItem item in start_items )
    {
      my_items.Add( item );
    }
  }
}
