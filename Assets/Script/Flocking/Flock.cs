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
    public float startingCount = 100;
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

    private GameObject flockControlNeutral;
    private GameObject flockControlGood;
    private GameObject flockControlBad;
    private GameObject Results;

    public bool endGame = false;
    public bool win = false;
    public bool lose = false;



    // Start is called before the first frame update
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
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
        flockControlNeutral = GameObject.Find("Flock");   // DO NOT DELET THIS LINE EVER
        flockControlGood = GameObject.Find("Flock Good"); // DO NOT DELET THIS LINE EVER
        flockControlBad = GameObject.Find("Flock Bad");   // DO NOT DELET THIS LINE EVER
    }

    public void OnDisable()
    {
        foreach (FlockAgent agent in agents)
        {
            Destroy(agent.gameObject);
        }
        agents = new List<FlockAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
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


        //   END GAME CONDITION STARTS 
        if (this.gameObject.tag == "FlockGood") //// start from here here here here here here
        {
            if (agents.Count == 0)
            {
                endGame = true;
                lose = true;
                currentGood = 0;
            }
            else
            {
                currentGood = agents.Count;
            }

        }
        if (this.gameObject.tag == "FlockBad")
        {
            if (agents.Count == 0)
            {
                StatusControl mention = GetComponent<StatusControl>();
                mention.AggTest = true;
                if (mention.AggTest == true)
                {
                    //calling lastStand fuction
                    mention.lastStandFunction();
                    if (mention.lastStand == false)
                    {
                        endGame = true;
                        win = true;
                    }

                }
                currentBad = 0;
                
            }
            else
            {
                currentBad = agents.Count;
            }

        }
        if (this.gameObject.tag == "FlockNeutral")
        {
            if (agents.Count == 0)
            {
                StatusControl mention = GetComponent<StatusControl>();
                mention.endGame = true;
                endGame = true;
                lose = true;
                mention.lose = true;
                
            }
            else
            {
                currentNeutral = agents.Count;
            }

        }

        //Reproduction starts
        if ((spanwCell == true) && (stopRep < 20))
        {
            stopRep++;
            number++;
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity * Random.Range(-size, size),
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

    public void AdjustStartingCount(float newstartingCount)
    {
        startingCount = newstartingCount;
    }


    public void StopGame()
    {
        if (endGame == true)
        {
            //call canvas game object and set other objects to be inactive.
            if (win == true)
            {
                Results = GameObject.Find("Success UI");
                Results.gameObject.SetActive(true);
                flockControlNeutral.gameObject.SetActive(false);
                flockControlBad.gameObject.SetActive(false);
                flockControlGood.gameObject.SetActive(false);
                
            }
            if (lose == true)
            {
                Results = GameObject.Find("Fail UI");
                Results.gameObject.SetActive(true);
                flockControlNeutral.gameObject.SetActive(false);
                flockControlBad.gameObject.SetActive(false);
                flockControlGood.gameObject.SetActive(false);
               
            }
        }

    }
}
