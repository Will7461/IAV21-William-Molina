using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotHandler : MonoBehaviour, IDropHandler
{
    public int index;
    /// <summary>
    /// If a sprite its dropped at a slot, set its attributes
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
		{
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<DragHandler>().removeFromIndexInventory();
            eventData.pointerDrag.GetComponent<DragHandler>().addToIndexInventory(index);
            eventData.pointerDrag.GetComponent<DragHandler>().setIndexAndLastPos(index, GetComponent<RectTransform>().anchoredPosition);
        }
    }

}
