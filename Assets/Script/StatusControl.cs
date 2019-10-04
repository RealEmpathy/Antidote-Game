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
    private float timer = 7;

    //will determine where to spaw a child of a cell
    public Transform spawPos;        //location to spaw children
    public GameObject CellPrefab;    // the prefab of the new cell

    public bool Reproduction = false;
    public bool Hunt = false;
    public bool flocking = false;
    public bool heal = false;
    public bool executeOnce = false;
    public bool lastStand = false;

    private GameObject scriptControl;


    private void Start()
    {
        scriptControl = this.gameObject; // DO NOT DELET THIS LINE EVER


        // ObjectCell.GetComponent<Seek>().enabled = false();
        //AIDestination aIDestination = GetComponent<aiDestination>();
        //CellsBehaviour cellsBehaviour = GetComponent<CellsBehaviour>();


        //TESTING EXAMPLE
        //TESTING EXAMPLE
        // mention is a reference to the other function
        // if you want to control another variable from another function you just use "  mention.TheNameOfTheVariable = New value  "
        //CellsBehaviour mention = GetComponent<CellsBehaviour>();

        //Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
        //mention.flee = true;

        //TESTING EXAMPLE
        //TESTING EXAMPLE


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
        if(lastStand == true)
        {

            lastStandFunction();
        }


        
/*         // will change code from the bottom 
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

        }*/
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
                if (lastStand == false) // come back here here here here here
                {
                    if (i > 20)
                    {
                        if (Reproduction == true)
                        {
                            SpawnChild();
                        }

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
        if(stamina > StaminaModifier)
        {
            // Target the the same cell with AllDestinationSetter (research on switching target) Maybe creating a bool to swtich Target inside the A*pathfinding. Like(private bool blue {target = targetblue; } /// private bool red //// private bool neutral ) 
            if(timer >= -1)
            {
                FindYourKind(timer);
            }
        }
        else
        {
            executeOnce = true;
            timer = 7;
            Reproduction = false;
            heal = true;
        }

    }
    float FindYourKind(float timer)
    {
        if (timer >= 7)
        {
            timer -= Time.deltaTime;
            Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
            mention.flee = true;
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
           
        if(timer <= 0)
        {
            // desable seek script 

            //TESTING THE SCRIPT BELOW
            scriptControl.GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;
            scriptControl.GetComponent<Pathfinding.AIPath>().enabled = false;
            scriptControl.GetComponent<Pathfinding.Seeker>().enabled = false;

            // switch script to flock
            scriptControl.GetComponent<FlockAgent>().enabled = true;
            flocking = true;
        }

        return timer;
    }
    void HuntYourEnemy()
    {
        // desable flock
        scriptControl.GetComponent<FlockAgent>().enabled = false;
        // switch script to seek
        scriptControl.GetComponent<Pathfinding.AIDestinationSetter>().enabled = true;
        scriptControl.GetComponent<Pathfinding.AIPath>().enabled = true;
        scriptControl.GetComponent<Pathfinding.Seeker>().enabled = true;
        
        //make sure we are hunting the right target
        Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
        mention.flee = false;
        flocking = false;
    }

    void HuntFunciton()
    {
        if(Hp>Survival)
        {    
            HuntYourEnemy();
        }
        else if (Hp<Survival)
        {
            Hunt = false;
            Reproduction = true;
            executeOnce = true;
        }

    }

    //destroy the game object
    void Healing()
    {
        if(Hp>Survival)
        {
            Hunt = true;
        }else
        if(Hp<Survival)
        {
            if(flocking == false)
            {
                FindYourKind(timer);
            }else
            {
                //continue flocking
            }

        }else
        if((stamina<=0)&&(Hp<Survival))
        {
            lastStand = true;
        }

    }

    void lastStandFunction()
    {
        if(HuntVar >= 65)
        {
            Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
            mention.lastStand = true;
        }
        else
        {

        }
    }

    void Dead()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
