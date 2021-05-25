using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
    
public class DragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
	private Vector2 lastPos;

	private int index;
	public void OnBeginDrag(PointerEventData eventData)
	{
		lastPos = GetComponent<RectTransform>().anchoredPosition;
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
	public void moveToLastPos()
	{
		GetComponent<RectTransform>().anchoredPosition = lastPos;
	}
	public Vector2 getLastPos()
	{
		return lastPos;
	}
	public int getIndex()
	{
		return index;
	}
	public void setIndexAndLastPos(int i, Vector2 pos)
	{
		index = i;
		lastPos = pos;
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
			int otherIndex = eventData.pointerDrag.GetComponent<DragHandler>().getIndex();
			Vector2 otherPos = eventData.pointerDrag.GetComponent<DragHandler>().getLastPos();
			string otherName = eventData.pointerDrag.name;

			GameManager.Instance.addToInventory(index, otherName);
			GameManager.Instance.addToInventory(otherIndex, gameObject.name);

			GetComponent<RectTransform>().anchoredPosition = otherPos;
			eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = lastPos;

			eventData.pointerDrag.GetComponent<DragHandler>().setIndexAndLastPos(index, lastPos);
			setIndexAndLastPos(otherIndex, otherPos);
		}
	}
	public void wasInstantiatedAt(int i)
	{
		index = i;
		lastPos = GetComponent<RectTransform>().anchoredPosition;
	}
}
