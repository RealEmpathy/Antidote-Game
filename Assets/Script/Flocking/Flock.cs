using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    public int size;

    public int currentGood;
    public int currentBad;
    public int currentNeutral;

    [Range(0, 30)]
    public static float startingCount = 100;
    const float AgentDensity = 0.02f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    public bool spanwCell;
    public int number = 100;
    public int stopRep = 0;
    public float GoodHuntVar;

    public GameObject NewCell;
    public GameObject Reference;

    public GameObject Panel;
    public GameObject Splicer;

    public GameObject GoodFlock;
    public GameObject BadFlock;
    public GameObject NeutralFlock;


    public bool endGame = false;
    public bool win = false;
    public bool lose = false;
    public bool lastStand = false;

    private float timer = 5;


    // Start is called before the first frame update
    private void Awake()
    {
        this.gameObject.SetActive(false);
        Panel.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        win = false;
        lose = false;
        endGame = false;
        timer = 5;

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity * Random.Range(-size, size),
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
            newAgent.gameObject.SetActive(true);
        }


    }

    public void OnDisable()
    {
        win = false;
        lose = false;
        endGame = false;
        foreach (FlockAgent agent in agents)
        {
            Destroy(agent.gameObject);
        }
        agents = new List<FlockAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        StopGameCall();

        foreach (FlockAgent agent in agents)
        {
            if (agent == null)
            {
                agents.Remove(agent);
            }

            List<Transform> context = GetNearbyObjects(agent);

            //FOR DEMO ONLY
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
         
        }


       
        

        //Reproduction starts
        if ((spanwCell == true) && (stopRep < 20))
        {
            stopRep++;
            number++;
            /*StatusControl mention2 = GetComponent<StatusControl>();
            Vector3 location = mention2.currentLocation;*/
            
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                NewCell.GetComponent<Transform>().position,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + number;
            newAgent.Initialize(this);
            agents.Add(newAgent);
            newAgent.gameObject.SetActive(true);

            if (this.gameObject.tag == "GoodCells")
            {
                Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
                mention.GoodCells.Add(this.gameObject);
                newAgent.gameObject.SetActive(true);
            }
            if (this.gameObject.tag == "BadCells")
            {
                Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
                mention.BadCells.Add(this.gameObject);
                newAgent.gameObject.SetActive(true);
            }
            if (this.gameObject.tag == "NeutralCells")
            {
                Pathfinding.AIDestinationSetter mention = GetComponent<Pathfinding.AIDestinationSetter>();
                mention.NeutralCells.Add(this.gameObject);
                newAgent.gameObject.SetActive(true);
            }

            spanwCell = false;
        }
        
        StopGame();
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        if (agent != null)
        {
            List<Transform> context = new List<Transform>();
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
            foreach (Collider2D c in contextColliders)
            {
                if (c != agent.AgentCollider)
                {
                    context.Add(c.transform);
                }
            }
            return context;
        }

        return null;
    }

    /*public void AdjustStartingCount(float newstartingCount)
    {
        startingCount = newstartingCount;
    }*/
    public void ResetRep(int reset = 0)
    {
        stopRep = reset;
    }

    public void AdjustGoodHunt(float newHunt)
    {
        GoodHuntVar = newHunt;
    }


    public void StopGameCall()
    {
        //this is in case all the Neutral cells died
        if (NeutralFlock.GetComponent<Flock>().agents.Count == 0)
        {
            GetCurrentCellNumbers();
            endGame = true;
            lose = true;
        }
        
        //this is in case all the good cells died
        if (GoodFlock.GetComponent<Flock>().agents.Count == 0)
        {
            // this first if is checking if the player is still wining without the Good cells
            if (BadFlock.GetComponent<Flock>().agents.Count > NeutralFlock.GetComponent<Flock>().agents.Count)
            {
                //if he is not winning the timer will start
                timer -= Time.deltaTime;
            }

            //in case the player wins without the good cells
            if (BadFlock.GetComponent<Flock>().agents.Count == 0)
            {
                GetCurrentCellNumbers();
                endGame = true;
                win = true;
            }

            // when the timer runs out
            if (timer <= 0)
            {
                GetCurrentCellNumbers();
                endGame = true;
                lose = true;
            }

        }


        //this is in case all the Bad cells died
        if (BadFlock.GetComponent<Flock>().agents.Count == 0)
        {
            if (GoodHuntVar >= StatusControl.GetHuntVar()) //last stand is true
            {
                lastStand = true;

                if (GoodFlock.GetComponent<Flock>().agents.Count == 0)
                {
                    GetCurrentCellNumbers();
                    endGame = true;
                    win = true;
                }
                else if (NeutralFlock.GetComponent<Flock>().agents.Count == 0)
                {
                    GetCurrentCellNumbers();
                    endGame = true;
                    lose = true;
                }

            }
            else //last stand is false
            {
                if (NeutralFlock.GetComponent<Flock>().agents.Count > GoodFlock.GetComponent<Flock>().agents.Count)
                {
                    GetCurrentCellNumbers();
                    endGame = true;
                    win = true;
                }
                else if (NeutralFlock.GetComponent<Flock>().agents.Count < GoodFlock.GetComponent<Flock>().agents.Count)
                {
                    GetCurrentCellNumbers();
                    // the line of code below might change to a "partial win scenario" later
                    endGame = true;
                    win = true;
                }
            }

        }
    }

    public void StopGame()
    {
        if (endGame == true)
        {
            //call canvas game object and set other objects to be inactive.
            if (win == true)
            {
                Panel.GetComponent<Hide>().showS = true;
                Panel.GetComponent<Hide>().finalGoodNum = currentGood;
                Panel.GetComponent<Hide>().finalBadNum = currentBad;
                Panel.GetComponent<Hide>().finalNeutralNum = currentNeutral;
                Splicer.SetActive(true);

                //script.enabled = true;
                NeutralFlock.SetActive(false);
                BadFlock.SetActive(false);
                GoodFlock.SetActive(false);

            }
            if (lose == true)
            {
                Panel.GetComponent<Hide>().showF = true;
                Panel.GetComponent<Hide>().finalGoodNum = currentGood;
                Panel.GetComponent<Hide>().finalBadNum = currentBad;
                Panel.GetComponent<Hide>().finalNeutralNum = currentNeutral;
                Splicer.SetActive(true);

                NeutralFlock.SetActive(false);
                BadFlock.SetActive(false);
                GoodFlock.SetActive(false);

            }
        }
    }
    
    public void GetCurrentCellNumbers()
    {
        currentNeutral = NeutralFlock.GetComponent<Flock>().agents.Count;
        currentBad = BadFlock.GetComponent<Flock>().agents.Count;
        currentGood = GoodFlock.GetComponent<Flock>().agents.Count;
    }

}
