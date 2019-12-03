using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide : MonoBehaviour
{
    public int finalNeutralNum;
    public int finalGoodNum;
    public int finalBadNum;

    public Text SDisplayNeutral;
    public Text SDisplayGood;
    public Text SDisplayBad;

    public Text FDisplayNeutral;
    public Text FDisplayGood;
    public Text FDisplayBad;

    public GameObject success;
    public GameObject fail;

    private GameObject flockControlNeutral;
    private GameObject flockControlGood;
    private GameObject flockControlBad;

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
        if (gameObject.activeInHierarchy == true)
        {
            if (showS)
            {
                success.SetActive(showS);
                write();
                showS = false;
            }
            if (showF)
            {
                fail.SetActive(showF);
                write();
                showF = false;
                
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
        
        if(showS == true)
        {
            SDisplayGood.text = finalGoodNum.ToString();
            SDisplayBad.text = finalBadNum.ToString();
            SDisplayNeutral.text = finalNeutralNum.ToString();
        }
        if (showF == true)
        {
            FDisplayGood.text = finalGoodNum.ToString();
            FDisplayBad.text = finalBadNum.ToString();
            FDisplayNeutral.text = finalNeutralNum.ToString();
        }

        
    }
}
