using UnityEngine;


public class GameManager : MonoBehaviour
{
  [SerializeField] private DayPart cur_day_part = DayPart.DAY;
  public DayPart getDayPart => cur_day_part;
  
  public static GameManager instance = null;
  
  
  private void Awake()
  {
    instance = this;
  }
}

public enum DayPart
{
  NONE,
  [InspectorName("Day")]
  DAY,
  [InspectorName("Red Night")]
  RED_NIGHT,
  [InspectorName("Black Night")]
  BLACK_NIGHT
}
