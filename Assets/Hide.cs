using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide : MonoBehaviour
{
    public int finalNeutralNum;
    public int finalGoodNum;
    public int finalBadNum;
    Text Display;

    private GameObject flockControlNeutral;
    private GameObject flockControlGood;
    private GameObject flockControlBad;

    public bool show;
    // Start is called before the first frame update
    void Awake()
    {
       this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        flockControlNeutral = GameObject.Find("Flock");   // DO NOT DELET THIS LINE EVER
        flockControlGood = GameObject.Find("Flock Good"); // DO NOT DELET THIS LINE EVER
        flockControlBad = GameObject.Find("Flock Bad");   // DO NOT DELET THIS LINE EVER

        this.gameObject.SetActive(true);
        if (this.gameObject.tag == "FlockGood")
        {
            Flock mention2 = flockControlGood.GetComponent<Flock>();
            finalGoodNum = mention2.currentGood;
            Display.text = finalGoodNum.ToString();
        }
        if (this.gameObject.tag == "FlockBad")
        {
            Flock mention2 = flockControlBad.GetComponent<Flock>();
            finalBadNum = mention2.currentBad;
            Display.text = finalBadNum.ToString();
        }
        if (this.gameObject.tag == "FlockNeutral")
        {
            Flock mention2 = flockControlNeutral.GetComponent<Flock>();
            finalNeutralNum = mention2.currentNeutral;
            Display.text = finalNeutralNum.ToString();
        }
    }
    void OnDisable()
    {
        this.gameObject.SetActive(false);
    }

}
