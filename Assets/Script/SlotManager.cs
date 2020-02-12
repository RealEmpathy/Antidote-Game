using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IDropHandler
{
    public GameObject parent;

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
        else
        {
            if(this.gameObject.tag != "Nodrop")
            {
                Destroy(transform.GetChild(0).gameObject);
                DragHandeler.DraggedItem.transform.SetParent(transform);
            }
            //parent.transform.GetChild(0).gameObject;
        }
    }
}
