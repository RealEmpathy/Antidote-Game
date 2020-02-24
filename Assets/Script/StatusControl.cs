using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusControl : MonoBehaviour
{
    //control hp and stamina lvls
    public float Hp;              //Not a constant value that will help with statements
    public static float MaxHp;          //static value that will not change
    public static float stamina;         //actual Max stamina
    private float MaxStamina;     //not a constant value: set by the player
    public float StaminaModifier;   //static value calculated when the program runs
    public static float HuntVar;           //static value set by the player (works togather with Susvival variable)
    public float Survival;          // static value that will be calculated when the program runs
    public float timer = 10;
    public int waitTime = 7;
    public static int aggressiveNumber = 80;

    public Vector3 currentLocation;

    public bool Reproduction = false;
    public bool Hunt = false;
    public bool flocking = false;
    public bool heal = false;
    public bool lastStand = false;
    public bool imortal = true;
    public bool startFight = false;
    private bool executeOnce = false;

    private GameObject flockControlNeutral;
    private GameObject flockControlGood;
    private GameObject flockControlBad;




    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        /*Debug.Log(HuntVar + " this is HuntVar");
        Debug.Log(stamina + " this is stamina");
        Debug.Log(MaxHp + " this is Maxhp");*/
        flockControlNeutral = GameObject.Find("Flock"); // reference to the game object called "Flock"
        flockControlGood = GameObject.Find("Flock Good"); // reference to the game object called "Flock Good"
        flockControlBad = GameObject.Find("Flock Bad"); // reference to the game object called "Flock Bad"

        Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
        mention.flocking = true;

        // this part generate random Values for BadCells
        if (this.gameObject.tag == "BadCells")
        {
            Hp = Random.Range(10, 70);  
            stamina = Random.Range(10, 80);
            HuntVar = Random.Range(50, 85);
        }
    }

    

    void Update()
    {
        if (!startFight)
        {
            SetStart();
        }


        if (!imortal)
        {
            HpManager(); //control and update Hp and stamina variables
            if(!Hunt)
            {
                Pathfinding.AIPath mention = GetComponent<Pathfinding.AIPath>();
                
                mention.maxSpeed = 15;
            }
            if (Hp <= 0)
            {// control when the cell die
                Dead();
                Debug.Log(this.name + " is Dead");
            }
            // switch between the fuctions
            if (Hunt)
            {
                //Debug.Log("Hunt is on");
                Pathfinding.AIPath mention = GetComponent<Pathfinding.AIPath>();
                mention.maxSpeed = 25;
                HuntFunciton();
            }
            if (Reproduction)
            {
               // Debug.Log("Reproduction is on");
                Reproduce();
            }
            if (heal)
            {
               // Debug.Log("Heal is on");
                Healing();
            }
            if (lastStand)
            {
                //Debug.Log("Last stand is on");
                lastStandFunction();
            }
            if((!lastStand)&& (!heal) && (!Reproduction) && (!Hunt))
            {
                if (MaxStamina > HuntVar)
                {
                    Reproduction = true;
                    Hunt = false;
                    heal = false;
                    lastStand = false;
                    executeOnce = false;
                    timer = waitTime;
                }
                else if (HuntVar > MaxStamina)
                {
                    Hunt = true;
                    Reproduction = false;
                    heal = false;
                    lastStand = false;
                    executeOnce = false;
                    timer = waitTime;
                }
            }
        }
        
        if(this.gameObject.tag == "GoodCells")
        {
            if (flockControlGood.GetComponent<Flock>().lastStand)
            {
                lastStand = true;
            }
        }
        if (this.gameObject.tag == "NeutralCells")
        {
            if (flockControlNeutral.GetComponent<Flock>().lastStand)
            {
                lastStand = true;
            }
        }
    }
 

    //logic when hitting another cell
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!imortal)
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
                    {
                        GreenCellAttack();
                        //Debug.Log("DamagingGreenCells");
                    }

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
                        // Debug.Log("DamagingGreenCells");
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!imortal)
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
                    if (lastStand)
                    {
                        GreenCellAttack();
                        //Debug.Log("DamagingGreenCells");
                    }
                        
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
                    if (lastStand)
                    {
                        DamagingGreenCells(); 
                       // Debug.Log("DamagingGreenCells");
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
            if(stamina < -5)
            {
                stamina = 0;
            }
        }
    }

    void SpawnChild()
    {
        //Reproducing
        stamina = stamina - 20;
        
        // fix version now spawn the cells inside the flock
        if (this.gameObject.tag == "GoodCells")
        {
            Flock mention2 = flockControlGood.GetComponent<Flock>();
            mention2.NewCell = this.gameObject;  
            mention2.spanwCell = true;
        }
        if (this.gameObject.tag == "BadCells")
        {
            Flock mention2 = flockControlBad.GetComponent<Flock>();
            mention2.spanwCell = true;
            mention2.NewCell = this.gameObject;
        }
        if (this.gameObject.tag == "NeutralCells")
        {
            Flock mention2 = flockControlNeutral.GetComponent<Flock>();
            mention2.spanwCell = true;
            mention2.NewCell = this.gameObject;
        }
        Reproduction = false;
    }

    void Reproduce()
    {
        // start flocking and reproduction
        if(stamina > StaminaModifier)
        {
            FindYourKind(timer);
        }
        else
        {
            timer = waitTime;
            Reproduction = false;
            executeOnce = false;
            Hunt = false;
            lastStand = false;
            heal = true;
        }

    }
   
    float FindYourKind(float timer)
    {
        if (timer >= waitTime)
        {
            if (executeOnce == false)
            {
                Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
                timer -= Time.deltaTime;
                mention.flocking = false;
                mention.flee = true;
                executeOnce = true;
            }
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
           
        if(timer <= 0)
        {
            Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
            mention.flocking = true;
        }
        return timer;
    }
    void HuntYourEnemy()
    {
        // desable flock
        Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
        mention.flee = false;
        mention.flocking = false;
        
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
            //switching to something else, reseting everything
            executeOnce = false;
            Hunt = false;
            heal = false;
            lastStand = false;
            timer = waitTime;
            Reproduction = true;
        }

    }

    void Healing()
    {
        if(Hp>Survival)
        {
            executeOnce = false;
            lastStand = false;
            timer = waitTime;
            Reproduction = false;
            heal = false;
            Hunt = true;
        }else
        if(Hp<Survival)
        {
            FindYourKind(timer);
        }else
        if((stamina <= 0)&&(Hp < Survival))
        {
            executeOnce = false;
            timer = waitTime;
            Reproduction = false;
            heal = false;
            Hunt = false;
            lastStand = true;
        }

    }

    public void lastStandFunction()
    {
        if(HuntVar >= aggressiveNumber)
        {
            Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
            mention.lastStand = true;
            mention.flocking = false;
            mention.flee = false;
            lastStand = true;
            //Debug.Log("aggressive lastStad is on");
        }
        else // the cell is not aggresive
        {
           // Debug.Log(" NOT aggressive lastStad is on");
        }
    }

    public void Dead()
    {
        Destroy(this.transform.gameObject);
        Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
        if (this.gameObject.tag == "GoodCells")
        {
            Flock mention2 = flockControlGood.GetComponent<Flock>();
            mention2.stopRep--;
            mention.GoodCells.Remove(this.gameObject);
            mention.ChangeTarget();
        }
        if (this.gameObject.tag == "BadCells")
        {
            Flock mention2 = flockControlBad.GetComponent<Flock>();
            mention2.stopRep--;
            mention.BadCells.Remove(this.gameObject);
            mention.ChangeTarget();
        }
        if (this.gameObject.tag == "NeutralCells")
        {
            Flock mention2 = flockControlNeutral.GetComponent<Flock>();
            mention2.stopRep--;
            mention.NeutralCells.Remove(this.gameObject);
            mention.ChangeTarget();
        }

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
    void GoodCellLastStandAttack()
    {
        int i = Random.Range(0, 100);
        if (i < HuntVar)
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
        if (i < HuntVar)
        {
            Hp = Hp - 5;
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

   /* public static void AdjustHunt(float newHunt)
    {
        HuntVar = newHunt;
    }
    public static void  AdjustReproduction(float newReproductin)
    {
        stamina = newReproductin;
    }

    public static void AdjustHP(float newHP)
    {
        Hp = newHP;
    }*/

    private void SetStart()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = waitTime; // wait time is a variable that can be modified for later testing

            // calculating the actual modifiers before running
            StaminaModifier = (100 - stamina);
            MaxHp = Hp;
            Survival = (100 - HuntVar);
            MaxStamina = stamina;
            Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
            mention.flocking = false;

            if (MaxStamina > HuntVar)
            {
                // start with:
                Reproduction = true;
                Hunt = false;
                heal = false;
                lastStand = false;
                executeOnce = false;
                timer = waitTime;
            }
            else if (HuntVar > MaxStamina)
            {
                // start with:
                Hunt = true;
                Reproduction = false;
                heal = false;
                lastStand = false;
                executeOnce = false;
                timer = waitTime;
            }
            startFight = true;
            imortal = false;
        }
    }
    public static int GetHuntVar()
    {
        return aggressiveNumber;
    }
}
