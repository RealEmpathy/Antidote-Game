﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour
{
    public int remainNeutral = 0;
    public int remainBad = 0;
    public int remainGood = 0;
    public int deadNeutral = 0;
    public int deadBad = 0;
    public int deadGood = 0;

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
        remainNeutral = GetComponent<Flock>().currentNeutral;
        remainBad = GetComponent<Flock>().currentBad;
        remainGood = GetComponent<Flock>().currentGood;
        
    }
}