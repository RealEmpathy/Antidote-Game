using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour
{
    private bool selected;
    public GameObject blue;
    public GameObject green;
    public GameObject red;
    public GameObject black;
    public GameObject yellow;
    public GameObject endPosition1;
    public GameObject endPosition2;
    public GameObject endPosition3;
    public GameObject endPosition4;
    public GameObject endPosition5;
     string target;

    Vector2 blueInitialPos;
    Vector2 greenInitialPos;
    Vector2 redInitialPos;
    Vector2 blackInitialPos;
    Vector2 yellowInitialPos;

    public int attachDistance = 50;

    void Start()
    {
        blueInitialPos = blue.transform.position;
        greenInitialPos = green.transform.position;
        redInitialPos = red.transform.position;
        blackInitialPos = black.transform.position;
        yellowInitialPos = yellow.transform.position;

    }
    private void OnMouseOver()
    {
        target = gameObject.tag;
        Debug.Log(target);
    }
    public void Drag()
    {
        if(target == "blue")
        {
            blue.transform.position = Input.mousePosition;
        }
        if (target == "green")
        {
            green.transform.position = Input.mousePosition;
        }
        if (target == "red")
        {
            red.transform.position = Input.mousePosition;
        }
        if (target == "black")
        {
            black.transform.position = Input.mousePosition;
        }
        if (target == "yellow")
        {
            yellow.transform.position = Input.mousePosition;
        }
    }

    public void Drop()
    {
        
        if (target == "blue")
        {
            float Distance1 = Vector3.Distance(blue.transform.position, endPosition1.transform.position);
            float Distance2 = Vector3.Distance(blue.transform.position, endPosition2.transform.position);
            float Distance3 = Vector3.Distance(blue.transform.position, endPosition3.transform.position);
            float Distance4 = Vector3.Distance(blue.transform.position, endPosition4.transform.position);
            float Distance5 = Vector3.Distance(blue.transform.position, endPosition5.transform.position);
            if(Distance1 < attachDistance)
            {
                blue.transform.position = endPosition1.transform.position;
            }
            else if (Distance2 < attachDistance)
            {
                blue.transform.position = endPosition2.transform.position;
            }
            else if (Distance3 < attachDistance)
            {
                blue.transform.position = endPosition3.transform.position;
            }
            else if (Distance4 < attachDistance)
            {
                blue.transform.position = endPosition4.transform.position;
            }
            else if (Distance5 < attachDistance)
            {
                blue.transform.position = endPosition5.transform.position;
            }
            else
            {
                blue.transform.position = blueInitialPos; // this is here the object should be deleted
            }
        }
        if (target == "green")
        {
            float Distance1 = Vector3.Distance(green.transform.position, endPosition1.transform.position);
            float Distance2 = Vector3.Distance(green.transform.position, endPosition2.transform.position);
            float Distance3 = Vector3.Distance(green.transform.position, endPosition3.transform.position);
            float Distance4 = Vector3.Distance(green.transform.position, endPosition4.transform.position);
            float Distance5 = Vector3.Distance(green.transform.position, endPosition5.transform.position);
            if (Distance1 < attachDistance)
            {
                green.transform.position = endPosition1.transform.position;
            }
            else if (Distance2 < attachDistance)
            {
                green.transform.position = endPosition2.transform.position;
            }
            else if (Distance3 < attachDistance)
            {
                green.transform.position = endPosition3.transform.position;
            }
            else if (Distance4 < attachDistance)
            {
                green.transform.position = endPosition4.transform.position;
            }
            else if (Distance5 < attachDistance)
            {
                green.transform.position = endPosition5.transform.position;
            }
            else
            {
                green.transform.position = greenInitialPos; // this is here the object should be deleted
            }
        }
        
        if (target == "red")
        {
            float Distance1 = Vector3.Distance(red.transform.position, endPosition1.transform.position);
            float Distance2 = Vector3.Distance(red.transform.position, endPosition2.transform.position);
            float Distance3 = Vector3.Distance(red.transform.position, endPosition3.transform.position);
            float Distance4 = Vector3.Distance(red.transform.position, endPosition4.transform.position);
            float Distance5 = Vector3.Distance(red.transform.position, endPosition5.transform.position);
            if (Distance1 < attachDistance)
            {
                red.transform.position = endPosition1.transform.position;
            }
            else if (Distance2 < attachDistance)
            {
                red.transform.position = endPosition2.transform.position;
            }
            else if (Distance3 < attachDistance)
            {
                red.transform.position = endPosition3.transform.position;
            }
            else if (Distance4 < attachDistance)
            {
                red.transform.position = endPosition4.transform.position;
            }
            else if (Distance5 < attachDistance)
            {
                red.transform.position = endPosition5.transform.position;
            }
            else
            {
                red.transform.position = redInitialPos; // this is here the object should be deleted
            }
        }
        if (target == "black")
        {
            float Distance1 = Vector3.Distance(black.transform.position, endPosition1.transform.position);
            float Distance2 = Vector3.Distance(black.transform.position, endPosition2.transform.position);
            float Distance3 = Vector3.Distance(black.transform.position, endPosition3.transform.position);
            float Distance4 = Vector3.Distance(black.transform.position, endPosition4.transform.position);
            float Distance5 = Vector3.Distance(black.transform.position, endPosition5.transform.position);
            if (Distance1 < attachDistance)
            {
                black.transform.position = endPosition1.transform.position;
            }
            else if (Distance2 < attachDistance)
            {
                black.transform.position = endPosition2.transform.position;
            }
            else if (Distance3 < attachDistance)
            {
                black.transform.position = endPosition3.transform.position;
            }
            else if (Distance4 < attachDistance)
            {
                black.transform.position = endPosition4.transform.position;
            }
            else if (Distance5 < attachDistance)
            {
                black.transform.position = endPosition5.transform.position;
            }
            else
            {
                black.transform.position = blackInitialPos; // this is here the object should be deleted
            }
        }
        if (target == "yellow")
        {
            float Distance1 = Vector3.Distance(yellow.transform.position, endPosition1.transform.position);
            float Distance2 = Vector3.Distance(yellow.transform.position, endPosition2.transform.position);
            float Distance3 = Vector3.Distance(yellow.transform.position, endPosition3.transform.position);
            float Distance4 = Vector3.Distance(yellow.transform.position, endPosition4.transform.position);
            float Distance5 = Vector3.Distance(yellow.transform.position, endPosition5.transform.position);
            if (Distance1 < attachDistance)
            {
                yellow.transform.position = endPosition1.transform.position;
            }
            else if (Distance2 < attachDistance)
            {
                yellow.transform.position = endPosition2.transform.position;
            }
            else if (Distance3 < attachDistance)
            {
                yellow.transform.position = endPosition3.transform.position;
            }
            else if (Distance4 < attachDistance)
            {
                yellow.transform.position = endPosition4.transform.position;
            }
            else if (Distance5 < attachDistance)
            {
                yellow.transform.position = endPosition5.transform.position;
            }
            else
            {
                yellow.transform.position = yellowInitialPos; // this is here the object should be deleted
            }
        }
    }


    /* void OnMouseOver()
     {
         Debug.Log(gameObject.tag);
     }*/

/*    void Update()
    {
        if (selected == true)
        {
            Debug.Log(gameObject.tag);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selected = false;
        }
    }*/
   



    /*   void Update()
       {
           if (selected == true)
           {
               Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
               transform.position = new Vector2(cursorPos.x, cursorPos.y);
           }

           if (Input.GetMouseButtonUp(0))
           {
               selected = false;
           }
       }
       */
}
