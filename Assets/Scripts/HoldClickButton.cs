using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnClickDown = null;
    public event Action OnClickUp = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickDown?.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        OnClickUp?.Invoke();
    }

}
