using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject DraggedItem;
    public GameObject cube1, cube2, cube3, cube4, cube5;
    Vector3 startPosition;
    Transform StartParent;



    public void OnBeginDrag(PointerEventData eventData)
    {

        DraggedItem = gameObject;
        var newObject = Instantiate(DraggedItem, startPosition, Quaternion.identity, DraggedItem.transform.parent);
        newObject.name = DraggedItem.name;
        startPosition = transform.position;
        StartParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //DraggedItem = null;
        // GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == StartParent)
        {
            //transform.position = startPosition;
            Destroy(DraggedItem);
        }



    }
    public void DeleteColor()
    {
        Destroy(this.gameObject);
    }
}
