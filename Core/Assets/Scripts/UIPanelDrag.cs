using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanelDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	Vector3 offset;

	public void OnBeginDrag(PointerEventData eventData)
	{
		offset = transform.position - Input.mousePosition;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition + offset;
	}
}
