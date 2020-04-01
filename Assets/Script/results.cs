using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class results : MonoBehaviour
{
    public int totalNeutral = 0;
    public int totalBad = 0;
    public int totalGood = 0;
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
        totalNeutral = GetComponent<Flock>().currentNeutral;
    }
}