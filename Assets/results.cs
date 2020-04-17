using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class results : MonoBehaviour
{
    public int remainNeutral = 0;
    public int remainBad = 0;
    public int remainGood = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void result()
    {
        remainNeutral = GetComponent<Hide>().finalNeutralNum;
        remainBad = GetComponent<Hide>().finalBadNum;
        remainGood = GetComponent<Hide>().finalGoodNum;
    }
}