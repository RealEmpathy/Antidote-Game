using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCode : MonoBehaviour
{
    private bool startCode = false;
    private static int Row = 3;
    private static int Col = 4;
    private int[,] solutions = new int[Row, Col];
    private int[] AllGuess = new int[Col];


    // Start is called before the first frame update
    void Start()
    {
      /*  if(startCode == false)
        {
            for(int i=0; i<Row; i++)
            {
                for (int j = 0; i<Col; j++)
                {
                    solutions[i, j] = Random.Range(1, 5);
                }
            }
            // for testing
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; i < Col; j++)
                {
                    Debug.Log(" " + solutions[Row, Col]);
                }
            }
            // for testing
            startCode = false;
        }*/
    }

    public void GetInput1(int[] guess1)
    {
        AllGuess[0] = guess1[0];
        Debug.Log(AllGuess[0]);
        Debug.Log("test");
    }
    public void GetInput2(int[] guess2)
    {
        AllGuess[1] = guess2[0];
        Debug.Log(AllGuess[1]);
    }
    public void GetInput3(int[] guess3)
    {
        AllGuess[2] = guess3[0];
        Debug.Log(AllGuess[2]);
    }
    public void GetInput4(int[] guess4)
    {

        AllGuess[3] = guess4[0];
        Debug.Log(AllGuess[3]);
    }
    public void GetInput5(int[] guess5)
    {
        AllGuess[4] = guess5[0];
        Debug.Log(AllGuess[4]);
    }
    public void CodeBundle()
    {

    }
}
