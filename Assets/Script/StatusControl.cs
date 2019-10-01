using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusControl : MonoBehaviour
{
    //control hp and stamina lvls
    public float Hp;              //Not a constant value that will help with statements
    private float MaxHp;          //static value that will not change
    public float stamina;         //actual Max stamina
    private float MaxStamina;     //not a constant value: set by the player
    public float StaminaModifier;   //static value calculated when the program runs
    public float HuntVar;           //static value set by the player (works togather with Susvival variable)
    public float Survival;          // static value that will be calculated when the program runs

    //will determine where to spaw a child of a cell
    public Transform spawPos;        //location to spaw children
    public GameObject CellPrefab;    // the prefab of the new cell



    private bool Reproduction = false;
    private bool Hunt = false;
    private bool flocking = false;


    private void Start()
    {
        // calculating the actual modifiers before running

        StaminaModifier = (100 - stamina);
        MaxHp = Hp;
        Survival = (100 - HuntVar);
        MaxStamina = stamina;
        

        if (MaxStamina > HuntVar)
        {
            // start with:
            //Reproduction = true;
        }
        else if (HuntVar > MaxStamina)
        {
            // start with:
            //Hunt = true;
        }
        //Option of starting with reproduction on
        //Reproduction = true;
        //Hunt = true;
    }



    void Update()
    {
        //desice what state are we in


        HpManager(); //control and update Hp and stamina variables

        if (Hp <= 0) // control when the cell die 
            Dead();


        // switch between the fuctions

        if ((stamina > StaminaModifier) && (HuntVar > StaminaModifier))
        {
            Reproduction = true;
            Hunt = false;
        }
        else
        if (HuntVar < StaminaModifier)
        {

            Reproduction = false;
            //Hunt();

        }
    }
 

    //logic when hitting another cell
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*Create a prefab for Good Cells with the exat name:
                                                          GoodCells
        */

        //if we are colliding with a Good cell
        if (other.gameObject.tag == "GoodCells")
        {
            //and the cell colliding is another Good cell
            if (this.gameObject.tag == "GoodCells")
            {
                int i = Random.Range(0, 100);
                if (i > 20) 
                {
                    if (Reproduction == true)
                    {
                        Reproduce();
                    }

                }
            }
            //and the cell colliding is a Bad cell
            if (this.gameObject.tag == "BadCells")
            {
                int i = Random.Range(0, 100);
                if (i > 50)//chace to hit might very according to aggressive lvls?
                {
                    if (stamina >= StaminaModifier)
                    {
                        stamina = 100;
                    }
                    else
                    {
                        stamina = stamina + 10;
                    }
                }
                if (i < 50)
                {
                    Hp = Hp - 20;
                }
            }
            
        }

        //if we are colliding with a Neutral cell
        if (other.gameObject.tag == "NeutralCells")
        {
            //and the cell colliding is a Bad cell
            if (this.gameObject.tag == "BadCells")
            {
                int i = Random.Range(0, 100);
                if (i > 20)
                {
                    if (stamina >= StaminaModifier)
                    {
                        stamina = 100;
                    }
                    else
                    {
                        stamina = stamina + 10;
                    }
                }
                if (i < 20)
                {
                    Hp = Hp - 5;
                }
            }

            //and the cell colliding is another Neutral cell
            if (this.gameObject.tag == "NeutralCells")
            {
                int i = Random.Range(0, 100);
                if (i > 10)
                {
                    if (Reproduction == true)
                    {
                        Reproduce();
                    }

                }
            }



        }

        //if we are colliding with a Bad cell
        if (other.gameObject.tag == "BadCells")
        {
            //and the cell colliding is a Good cell
            if (this.gameObject.tag == "GoodCells")
            {
                int i = Random.Range(0, 100);
                if (i > 50)
                {
                    if (stamina >= StaminaModifier)
                    {
                        stamina = 100;
                    }
                    else
                    {
                        stamina = stamina + 10;
                    }
                }
                if (i < 50)
                {
                    Hp = Hp - 20;
                }
            }

            //and the cell colliding is a Neutral cell
            if (this.gameObject.tag == "NeutralCells")
            {
                int i = Random.Range(0, 100);
                if (i > 90)
                {
                    if (stamina >= StaminaModifier)
                    {
                        stamina = 100;
                    }
                    else
                    {
                        stamina = stamina + 10;
                    }
                }
                if (i < 90)
                {
                    Hp = Hp - 5;
                }
            }

            //and the cell colliding is another Bad cell
            if (this.gameObject.tag == "BadCells")
            {
                int i = Random.Range(0, 100);
                if (i > 20)
                {
                    if (Reproduction == true)
                    {
                        Reproduce();
                    }

                }
            }

            

        }

    }

    void StateSwitcher()
    {
        if(Hunt == true)
        {
            //GetComponent("Flock Agent").enable = false;
        }

    }



    void HpManager()
    {
        if (Hp > 0 && stamina > 0)
        {
            stamina -= Time.deltaTime;
            if (Hp >= MaxHp)// check to not allow over healing
            {
                // Hp is at it's current Max
            }
            else
            {
                //healing because Hp is not full
                Hp += Time.deltaTime;
            }
        }
        if (Hp > 0 && stamina <= 0)
        {// Check when Stamina runs out
            Hp -= Time.deltaTime;
        }
    }



    void Reproduce()
    {
        // Target the the same cell with AllDestinationSetter (research on switching target) Maybe creating a bool to swtich Target inside the A*pathfinding. Like(private bool blue {target = targetblue; } ... /// private bool red //// private bool neutral ) 
        // switch script to flock
        // desable seek script
        //start flocking and reproduction


        stamina = stamina - 20;
        spawPos = CellPrefab.GetComponent<Transform>();
        Instantiate(CellPrefab, this.transform.position, this.transform.rotation);
        Reproduction = false;
    }

    /*void Hunt()
    {
        // desable flock
        // switch script to seek
        //start flocking and reproduction

    }*/

    //destroy the game object
    void Dead()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
