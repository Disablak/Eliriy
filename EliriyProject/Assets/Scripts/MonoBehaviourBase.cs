using UnityEngine;


public class MonoBehaviourBase : MonoBehaviour
{
  private bool inited = false;

  public RectTransform rectTransform => (RectTransform) transform;

  protected virtual void initMyComponents() {}

  protected void initComponents()
  {
    if ( inited )
      return;
    
    initMyComponents();
    inited = true;
  }
}
