﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvSlot : MonoBehaviour, IDropHandler
{
	public GameObject item
	{ 
		get
		{
			if (transform.childCount > 0)
			{
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (!item)
		{
			if (InvItemDrag.draggedItem != null)
			{
				InvItemDrag.draggedItem.transform.SetParent(transform);
			}
		}
	}
}
