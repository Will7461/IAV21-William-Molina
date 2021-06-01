using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Class used by inventory background
/// </summary>
public class InventoryBGHandler : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// We reset the sprite position if it is dropped out of the inventory
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragHandler>().moveToLastPos();
        }
    }
}
