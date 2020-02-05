using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IDropHandler
{
  public GameObject colors
    {
        get
        {
            if(transform.childCount>0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        if(!colors)
        {
            DragHandeler.DraggedItem.transform.SetParent(transform);
        }
    }
}
