using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class Clickable : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action onClick = delegate {};
    
    public event Action<PointerEventData> onBeginDrag = data => { };
    public event Action<PointerEventData> onEndDrag   = data => { };
    public event Action<PointerEventData> onDrag      = data => { };

    
    void IPointerClickHandler.OnPointerClick( PointerEventData _ )
    {
        if ( EventSystem.current.IsPointerOverGameObject() )
        {
            onClick?.Invoke();
        }
    }
    
    void IBeginDragHandler.OnBeginDrag( PointerEventData eventData )
    {
        if ( EventSystem.current.IsPointerOverGameObject() )
        {
            onBeginDrag?.Invoke( eventData );
        }
    }
    
    void IDragHandler.OnDrag( PointerEventData eventData )
    {
        if ( EventSystem.current.IsPointerOverGameObject() )
        {
            onDrag?.Invoke( eventData );
        }
    }

    void IEndDragHandler.OnEndDrag( PointerEventData eventData )
    {
        if ( EventSystem.current.IsPointerOverGameObject() )
        {
            onEndDrag?.Invoke( eventData );
        }
    }
}
