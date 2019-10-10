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
    public float timer = 7;
    public int waitTime = 3;

    //will determine where to spaw a child of a cell
    public Transform spawPos;        //location to spaw children
    public GameObject CellPrefab;    // the prefab of the new cell

    public bool Reproduction = false;
    public bool Hunt = false;
    public bool flocking = false;
    public bool heal = false;
    public bool executeBeforeStart = false;
    public bool lastStand = false;
    public bool imortal = true;
    public bool startFight = false;
    private bool executeOnce = false;

    private GameObject scriptControl;
    private GameObject flockControlNeutral;
    private GameObject flockControlGood;
    private GameObject flockControlBad;

    

    private void Start()
    {
        scriptControl = this.gameObject; // DO NOT DELET THIS LINE EVER

        flockControlNeutral = GameObject.Find("Flock");   // DO NOT DELET THIS LINE EVER
        flockControlGood = GameObject.Find("Flock Good"); // DO NOT DELET THIS LINE EVER
        flockControlBad = GameObject.Find("Flock Bad");   // DO NOT DELET THIS LINE EVER



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




    }

   

    void Update()
    {
        if (startFight == false)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                timer = waitTime;
                // might have to take this out
                if (MaxStamina > HuntVar)
                {
                    // start with:
                    Reproduction = true;
                    Hunt = false;
                    heal = false;
                    lastStand = false;
                }
                else if (HuntVar > MaxStamina)
                {
                    // start with:
                    Hunt = true;
                    Reproduction = false;
                    heal = false;
                    lastStand = false;
                }
                startFight = true;
                imortal = false;

                // calculating the actual modifiers before running
                StaminaModifier = (100 - stamina);
                MaxHp = Hp;
                Survival = (100 - HuntVar);
                MaxStamina = stamina;
            }
           /* if (executeBeforeStart == false)
            {
                JustFlock();
                flocking = true;
                executeBeforeStart = true;
            }*/
        }
        

        if (imortal == false)
        {
            HpManager(); //control and update Hp and stamina variables

            if (Hp <= 0)
            {// control when the cell die
                Dead();
                Debug.Log(this.name + " is Dead");
            }
            // switch between the fuctions
            if (Hunt == true)
            {
                Debug.Log("Hunt is on");
                HuntFunciton();
            }
            if (Reproduction == true)
            {
                Debug.Log("Reproduction is on");
                Reproduce();
            }
            if (heal == true)
            {
                Debug.Log("Heal is on");
                Healing();
            }
            if (lastStand == true)
            {
                Debug.Log("Last stand is on");
                lastStandFunction();
            }
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
        if(imortal == false)
        {
            //if we are colliding with a Good cell
            if (other.gameObject.tag == "GoodCells")
            {
                //if we are colliding with a Good cell
                //and the cell colliding is another Good cell
                if (this.gameObject.tag == "GoodCells")
                {
                    CellReproduction();
                }

                //if we are colliding with a Good cell
                //and the cell colliding is Neutral cell
                if (this.gameObject.tag == "NeutralCells")
                {
                    if (lastStand == true)
                        DamagingGreenCells();
                }

                //if we are colliding with a Good cell
                //and the cell colliding is a Bad cell
                if (this.gameObject.tag == "BadCells")
                {
                    BadCellAttack();
                }

            }

            //if we are colliding with a Neutral cell
            if (other.gameObject.tag == "NeutralCells")
            {
                //if we are colliding with a Neutral cell
                //and the cell colliding is a Bad cell
                if (this.gameObject.tag == "BadCells")
                {
                    DamagingGreenCells();
                }
                //if we are colliding with a Neutral cell
                //and the cell colliding is another Neutral cell
                if (this.gameObject.tag == "NeutralCells")
                {
                    CellReproduction();
                }
                //if we are colliding with a Neutral cell
                //and the cell colliding is another Good cell
                if (this.gameObject.tag == "GoodCells")
                {
                    if (lastStand == true)
                    {
                        DamagingGreenCells();
                    }

                }



            }

            //if we are colliding with a Bad cell
            if (other.gameObject.tag == "BadCells")
            {
                //if we are colliding with a Bad cell
                //and the cell colliding is a Good cell
                if (this.gameObject.tag == "GoodCells")
                {
                    GoodCellAttack();
                }

                //if we are colliding with a Bad cell
                //and the cell colliding is a Neutral cell
                if (this.gameObject.tag == "NeutralCells")
                {
                    GreenCellAttack();
                }

                //if we are colliding with a Bad cell
                //and the cell colliding is another Bad cell
                if (this.gameObject.tag == "BadCells")
                {
                    CellReproduction();
                }



            }
        }
        

    }


    void HpManager()
    {
        if (Hp > 0 && stamina > 0)
        {
            stamina -= Time.deltaTime;
            if (Hp >= MaxHp)  // check to not allow over healing
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
        {   // Check when Stamina runs out
            Hp -= Time.deltaTime;
        }
    }

    void SpawnChild()
    {
        //Reproducing
        stamina = stamina - 20;
        /*spawPos = CellPrefab.GetComponent<Transform>();
        Instantiate(CellPrefab, this.transform.position, this.transform.rotation);*/
        if (this.gameObject.tag == "GoodCells")
        {
            Flock mention2 = flockControlGood.GetComponent<Flock>();
            mention2.spanwCell = true;
        }
        if (this.gameObject.tag == "BadCells")
        {
            Flock mention2 = flockControlBad.GetComponent<Flock>();
            mention2.spanwCell = true;
        }
        if (this.gameObject.tag == "NeutralCells")
        {
            Flock mention2 = flockControlNeutral.GetComponent<Flock>();
            mention2.spanwCell = true;
        }


        Reproduction = false;
    }

 
    void Reproduce()
    {
        
        // start flocking and reproduction
        if(stamina > StaminaModifier)
        {
            // Target the same cell with AllDestinationSetter (research on switching target) Maybe creating a bool to swtich Target inside the A*pathfinding. Like(private bool blue {target = targetblue; } /// private bool red //// private bool neutral ) 
            if(timer >= -1)
            {
                if(executeOnce == false)
                {
                    FindYourKind(timer);
                    executeOnce = true;
                }
                    
            }
        }
        else
        {
            //executeOnce = true;
            timer = waitTime;
            Reproduction = false;
            heal = true;
            executeOnce = false;
        }

    }
    void JustFlock()
    {
        //TESTING THE SCRIPT BELOW
        scriptControl.GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;
        scriptControl.GetComponent<Pathfinding.AIPath>().enabled = false;
        scriptControl.GetComponent<Pathfinding.Seeker>().enabled = false;

        // switch script to flock
        scriptControl.GetComponent<FlockAgent>().enabled = true;
    }
    float FindYourKind(float timer)
    {
        if (timer >= waitTime)
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
           // executeOnce = true;
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
            JustFlock(); //where the cell goes ot die
        }
    }

    void Dead()
    {
        Destroy(this.transform.gameObject);
    }


    void GoodCellAttack()
    {
        int i = Random.Range(0, 100);
        if (i > 50)
        {
            if (stamina >= MaxStamina)
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

    void BadCellAttack()
    {
        int i = Random.Range(0, 100);
        if (i > 50)
        {
            if (stamina >= MaxStamina)
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
            Hp = Hp - 20; // amount of damage
        }
    }

    void GreenCellAttack()
    {
        int i = Random.Range(0, 100);
        if (i > 80)
        {
            if (stamina >= MaxStamina)
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
            Hp = Hp - 20;// amount of damage done by neutral
        }
    }

    void DamagingGreenCells()
    {
        int i = Random.Range(0, 100);
        if (i > 20)
        {
            if (stamina >= MaxStamina)
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
            Hp = Hp - 5;// amount of damage done by neutral
        }
    }


    void CellReproduction()
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
