using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject draggedItem;
	Vector3 startPos;
	Transform startParent;
	
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		draggedItem = gameObject;
		startPos = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		draggedItem = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent == startParent)
		{
			transform.position = startPos;
		}
	}

	private void OnMouseDown()
	{
		this.transform.position = new Vector3(this.transform.position.x + Input.GetAxis("Mouse X"), this.transform.position.y + Input.GetAxis("Mouse Y"), this.transform.position.z);
	}
}
