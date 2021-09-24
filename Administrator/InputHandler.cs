using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(
            pointerEventData.pressPosition.x,
            pointerEventData.pressPosition.y,
            Camera.main.nearClipPlane));
        Administrator.Instance.Click(worldPoint);
    }
}
