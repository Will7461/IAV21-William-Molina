using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
    
/// <summary>
/// Drag handler of a dragable sprite
/// </summary>
public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
	/// <summary>
	/// Where the sprite has been taken
	/// </summary>
	private Vector2 lastPos;
	/// <summary>
	/// Inventory index to which this sprite belongs
	/// </summary>
	private int index;
	/// <summary>
	/// Save last position to use in case we drop the sprite in a incorrect position, set lower alpha and avoid blocking raycast
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
		lastPos = GetComponent<RectTransform>().anchoredPosition;
		GetComponent<CanvasGroup>().alpha = 0.6f;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	/// <summary>
	/// Move the sprite following the mouse cursor
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}
	/// <summary>
	/// Return the original alpha value and the interaction with raycasts
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
		GetComponent<CanvasGroup>().alpha = 1f;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
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
	/// <summary>
	/// If we drop on another sprite, we switch positions
	/// </summary>
	/// <param name="eventData"></param>
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
