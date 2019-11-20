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

    public GameObject success;
    public GameObject fail;

    private GameObject flockControlNeutral;
    private GameObject flockControlGood;
    private GameObject flockControlBad;

    public GameObject GoodFlock;
    public GameObject NeutralFlock;
    public GameObject BadFlock;

    public bool showS = false;
    public bool showF = false;
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.SetActive(true);
        success.SetActive(false);
        fail.SetActive(false);
    }
    public void Update()
    {
        if (gameObject.activeInHierarchy == true) // here here here here here here 
        {
            if (showS == true)
            {
                success.SetActive(showS);
                showS = false;
                write();
            }
            if (showF == true)
            {
                fail.SetActive(showF);
                showF = false;
                write();
            }
        }

    }
    private void OnEnable()
    {
        this.gameObject.SetActive(true);
        success.SetActive(false);
        fail.SetActive(false);
        
    }
    void OnDisable()
    {
        success.SetActive(false);
        fail.SetActive(false);

    }



    void write()
    {
        //this.gameObject.GetComponent<Hide>().enabled = true;
        flockControlNeutral = GameObject.Find("Flock");   // DO NOT DELET THIS LINE EVER
        flockControlGood = GameObject.Find("Flock Good"); // DO NOT DELET THIS LINE EVER
        flockControlBad = GameObject.Find("Flock Bad");   // DO NOT DELET THIS LINE EVER

        //this.gameObject.SetActive(true);
          
        finalGoodNum = GoodFlock.GetComponent<Flock>().agents.Count;
        Display.text = finalGoodNum.ToString();
        
          
        finalBadNum = BadFlock.GetComponent<Flock>().agents.Count;
        Display.text = finalBadNum.ToString();

        finalNeutralNum = NeutralFlock.GetComponent<Flock>().agents.Count;
        Display.text = finalNeutralNum.ToString();
        
    }
}
