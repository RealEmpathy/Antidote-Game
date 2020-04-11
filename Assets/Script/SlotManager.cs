using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour, IDropHandler
{
    public GameObject ThisSlot;
    public string number;
    public bool update;
/*    public int count = 0;
    public int insideNotColor = 0;
    public int insideYesColor = 0;*/


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
   /* private void Start()
    {
        if(this.gameObject.tag == "Nodrop")
        {

        }
        else
        {
            ThisSlot.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        
    }*/

    public void OnDrop(PointerEventData eventData)
    {
        DragHandeler.DraggedItem.transform.SetParent(transform); // this makes the object stay in the slots on the right
        if (ThisSlot.gameObject.transform.GetChild(0) != null)
            {
                Destroy(transform.GetChild(0).gameObject);
                DragHandeler.DraggedItem.transform.SetParent(transform);
                //ThisSlot.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        number = ThisSlot.gameObject.transform.GetChild(0).gameObject.name;
        UpdateColor();
/*
        if (!colors)
        {
            //update = false;
            
          
            DragHandeler.DraggedItem.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            number = ThisSlot.gameObject.transform.GetChild(0).gameObject.name;
            UpdateColor(number);
            insideNotColor++;
        }
        else
        {
           // update = true;
            if (this.gameObject.tag != "Nodrop")
            {
                Destroy(transform.GetChild(0).gameObject);
                DragHandeler.DraggedItem.transform.SetParent(transform);
                //ThisSlot.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            //parent.transform.GetChild(0).gameObject;

            //DragHandeler.DraggedItem.GetComponent<Image>().color = new Color(0, 0, 0, 0); // make the dragged object invisible
            number = ThisSlot.gameObject.transform.GetChild(0).gameObject.name;
            UpdateColor(number);
            Debug.Log(ThisSlot.gameObject.GetComponent<Image>().color);
            insideYesColor++;
        }*/
    }

    private void UpdateColor ()
    {
        if (number == "1")
        {
            ThisSlot.gameObject.GetComponent<Image>().color = new Color(0, 0, 1, 1);
        }
        else
           if (number == "2")
        {
            ThisSlot.gameObject.GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
           if (number == "3")
        {
            ThisSlot.gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 1);
        }
        else
           if (number == "4")
        {
            ThisSlot.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
        else
           if (number == "5")
        {
            ThisSlot.gameObject.GetComponent<Image>().color = new Color(1, 1, 0, 1);
        }

    }

}
