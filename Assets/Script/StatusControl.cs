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
    private float timer = 10;

    //will determine where to spaw a child of a cell
    public Transform spawPos;        //location to spaw children
    public GameObject CellPrefab;    // the prefab of the new cell

    private bool Reproduction = false;
    private bool Hunt = false;
    private bool flocking = false;
    private bool heal = false;
    private bool executeOnce = false;


    private void Start()
    {
        // ObjectCell.GetComponent<Seek>().enabled = false();
        //AIDestination aIDestination = GetComponent<aiDestination>();
        //CellsBehaviour cellsBehaviour = GetComponent<CellsBehaviour>();


        //THE EXAMPLE WORKED
        //THE EXAMPLE WORKED
        // mention is a reference to the other function
       // if you want to control another variable from another function you just use "  mention.TheNameOfTheVariable = New value  "
        CellsBehaviour mention = GetComponent<CellsBehaviour>();
        mention.testing = true;
        //THE EXAMPLE WORKED
        //THE EXAMPLE WORKED


        // calculating the actual modifiers before running

        StaminaModifier = (100 - stamina);
        MaxHp = Hp;
        Survival = (100 - HuntVar);
        MaxStamina = stamina;
        

        if (MaxStamina > HuntVar)
        {
            // start with:
            Reproduction = true;
        }
        else if (HuntVar > MaxStamina)
        {
            // start with:
            Hunt = true;
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
        if (Hunt == true)
        {
            HuntFunciton();
        }
        if(Reproduction ==  true)
        {
            Reproduce();
        }
        if(heal == true)
        {
            Healing();
        }


        
         // will change code from the bottom 
        //or maybe delete it 
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
                        SpawnChild();
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
                        SpawnChild();
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
                        SpawnChild();
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

    void SpawnChild()
    {
        //Reproducing
        stamina = stamina - 20;
        spawPos = CellPrefab.GetComponent<Transform>();
        Instantiate(CellPrefab, this.transform.position, this.transform.rotation);
        Reproduction = false;
    }

    void Reproduce()
    {
        
        // start flocking and reproduction
        if(stamina> StaminaModifier)
        {
            //ObjectCell.GetComponent<AIDestinationSetter>().enabled = false;
            

            if (executeOnce == true)
            {
                // Target the the same cell with AllDestinationSetter (research on switching target) Maybe creating a bool to swtich Target inside the A*pathfinding. Like(private bool blue {target = targetblue; } ... /// private bool red //// private bool neutral ) 
               
                // desable seek script    
                // switch script to flock

                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    executeOnce = false;
                }  
            }
            
        }
        else
        {
            executeOnce = true;
            timer = 10;
            Reproduction = false;
        }

    }

    void HuntFunciton()
    {
        // desable flock
        // switch script to seek
        //start flocking and reproduction


        // include a fuction to change the bool condition

        //Hunt = false;
        //Reproduction == true;
        //executeOnce = true;
    }

    //destroy the game object
    void Healing()
    {

    }

    void Dead()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
