using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCode : MonoBehaviour
{
    private bool startCode = false;
    private static int Row = 4;
    private static int Col = 5;
    private int[,] solutions = new int[Row, Col];
    private int[] AllGuess = new int[5];
    private int[] list1 = new int[5];
    private int[] list2 = new int[5];
    private int[] percentage = new int[5];
    public int aggro;
    public int reproduction;
    public int HP;
    public int cellNum;


    // Start is called before the first frame update
    void Start()
    {
        if (startCode == false)
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    solutions[i, j] = Random.Range(1, 5);
                }
            }
            // for testing
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Debug.Log(" " + solutions[i, j]);
                }
            }
            // for testing
            startCode = false;
        }
    }

    public void GetInput1(string guess1)
    {
        int i = int.Parse(guess1);
        AllGuess[0] = i;
    }
    public void GetInput2(string guess2)
    {
        int i = int.Parse(guess2);
        AllGuess[1] = i;
    }
    public void GetInput3(string guess3)
    {
        int i = int.Parse(guess3);
        AllGuess[2] = i;
    }
    public void GetInput4(string guess4)
    {

        int i = int.Parse(guess4);
        AllGuess[3] = i;
    }
    public void GetInput5(string guess5)
    {
        int i = int.Parse(guess5);
        AllGuess[4] = i;
        /*for (int j = 0; j < 5; j++)
        {
            Debug.Log(AllGuess[j]);
        }*/
        CodeBundle();
    }


    public void CodeBundle()
    {
        int match = 0;
        int count = 0;
        int halfMatches = 0;
        // EXPLANING THE 2D ARRAY CALLED: 
        // *** SOLUTIONS ***
        // first roll is for aggro
        // second roll is for reproduction
        // third roll is for Hp
        // fourth roll is for Number of cells
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Col; j++)
            {
                if (AllGuess[j] == solutions[i, j])
                {
                    match++;
                }
                else // if there is no match put in a different list and to a different counter
                {
                    list1[count] = AllGuess[j];
                    list2[count] = solutions[i, j];
                    count++;
                }

            }
            for(int g = 0; g<count; g++)
            {
                for (int h = 0; h < count; h++)
                {
                    if(list1[g]==list2[h])
                    {
                        halfMatches++;
                        list2[h] = 99;
                    }
                }
            }
            if (match == 0 && halfMatches == 0)
            {
                percentage[i] = 5;
            }
            else
            {
                percentage[i] = (match * 20) + (halfMatches * 10);
            }
            match = 0;
            halfMatches = 0;
            count = 0;
        }

        for (int j = 0; j < 4; j++)
        {
            Debug.Log("this is percentage" + percentage[j]);
        }
    }
    

}
