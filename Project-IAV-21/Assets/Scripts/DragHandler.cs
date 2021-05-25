using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
    
public class DragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
	private Vector3 lastPos;

	private int index;
	public void OnBeginDrag(PointerEventData eventData)
	{
		lastPos = transform.position;
		GetComponent<CanvasGroup>().alpha = 0.6f;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		GetComponent<CanvasGroup>().alpha = 1f;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		
	}
	public void setToLastPos()
	{
		transform.position = lastPos;
	}
	public void addToIndexInventory(int i)
	{
		index = i;
		GameManager.Instance.addToInventory(index, gameObject.name);
	}
	public void removeFromIndexInventory()
	{
		if (index == -1) return;
		GameManager.Instance.removeFromInventory(index);
		index = -1;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag != null)
		{
			eventData.pointerDrag.GetComponent<DragHandler>().setToLastPos();
		}
	}
}
