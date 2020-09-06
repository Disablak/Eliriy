using System;
using UnityEngine;


public class ColliderClickable : MonoBehaviour
{
  public event Action onMouseDrag = delegate {};
  public event Action onMouseDown = delegate {};
  public event Action onMouseUp   = delegate {};
  public event Action onMouseOver = delegate {};
  
  
  private void OnMouseDrag()
  {
    onMouseDrag?.Invoke();
  }

  private void OnMouseDown()
  {
    onMouseDown?.Invoke();
  }

  private void OnMouseUp()
  {
    onMouseUp?.Invoke();
  }

  private void OnMouseOver()
  {
    onMouseOver?.Invoke();
  }
}
