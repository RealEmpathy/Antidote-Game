using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding
{
    /// <summary>
    /// Sets the destination of an AI to the position of a specified object.
    /// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
    /// This component will then make the AI move towards the <see cref="target"/> set on this component.
    ///
    /// See: <see cref="Pathfinding.IAstarAI.destination"/>
    ///
    /// [Open online documentation to see images]
    /// </summary>
    [UniqueComponent(tag = "ai.destination")]
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
    public class AIDestinationSetter : VersionedMonoBehaviour
    {
        /// <summary>The object that the AI should move to</summary>
      
        public Transform target;   /// THIS WERE TAKING changed to private to test how the tags are going to work
        public Transform target2Flee;  /// THIS WERE TAKING changed to private to test how the tags are going to work
        public Transform target3LastStand;

        public GameObject GoodCell;
        public List<GameObject> GoodCells = new List<GameObject>();

        public GameObject BadCell;
        public List<GameObject> BadCells = new List<GameObject>();

        public GameObject NeutralCell;
        public List<GameObject> NeutralCells = new List<GameObject>();

        public bool flee = false;
        public bool lastStand = false;
        public int switching1 = 0;
        public int switching2 = 0;
        public int switching3 = 0;
        IAstarAI ai;

        void OnEnable()
        {
            ai = GetComponent<IAstarAI>();
            // Update the destination right before searching for a path as well.
            // This is enough in theory, but this script will also update the destination every
            // frame as the destination is used for debugging and may be used for other things by other
            // scripts as well. So it makes sense that it is up to date every frame.
            if (ai != null) ai.onSearchPath += Update;
        }

        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }


        void Start()
        {
            CreateList();
            FindTarget();
        }

        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
        {
            updateList();
            ChangeTarget();
            

            //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE
            if (lastStand == false)
            {
                if (flee == false)
                {
                    if (target != null && ai != null) ai.destination = target.position;
                }
                //enable target2Flee for the new target to take place
                if (flee == true)
                {
                    if (target2Flee != null && ai != null) ai.destination = target2Flee.position;
                }
            }
            if (lastStand == true)
            {
                if (target3LastStand != null && ai != null) ai.destination = target3LastStand.position;
            }
            //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE 

            
/*
            if (lastStand == false)
            {
                if (flee == false)
                {
                    if (this.gameObject.tag == "GoodCells")
                    {
                        //Debug.Log("this is a Good cell hunting Bad cells");
                        target = GameObject.FindGameObjectWithTag("BadCells").GetComponent<Transform>();
                    }

                    if (this.gameObject.tag == "BadCells")
                    {
                        //Debug.Log("this is a Bad cell hunting Neutral cells");
                        target = GameObject.FindGameObjectWithTag("NeutralCells").GetComponent<Transform>();
                    }
                    if (this.gameObject.tag == "NeutralCells")
                    {
                        //Debug.Log("this is a Neutral cell hunting Bad cells");
                        target = GameObject.FindGameObjectWithTag("BadCells").GetComponent<Transform>();
                    }
                }

                if (flee == true)
                {
                    if (this.gameObject.tag == "GoodCells")
                    {
                        //Debug.Log("this is a Good cell hunting Bad cells");
                        target = GameObject.FindGameObjectWithTag("GoodCells").GetComponent<Transform>();
                    }

                    if (this.gameObject.tag == "BadCells")
                    {
                        //Debug.Log("this is a Bad cell hunting Neutral cells");
                        target = GameObject.FindGameObjectWithTag("BadCells").GetComponent<Transform>();
                    }
                    if (this.gameObject.tag == "NeutralCells")
                    {
                        //Debug.Log("this is a Neutral cell hunting Bad cells");
                        target = GameObject.FindGameObjectWithTag("NeutralCells").GetComponent<Transform>();
                    }
                }
            }
            else
            if (lastStand == true)
            {
                if (this.gameObject.tag == "GoodCells")
                {
                    //Debug.Log("this is a Good cell hunting Bad cells");
                    target = GameObject.FindGameObjectWithTag("NeutralCells").GetComponent<Transform>();
                }

                if (this.gameObject.tag == "BadCells")
                {
                    //Debug.Log("this is a Bad cell hunting Neutral cells");
                    target = GameObject.FindGameObjectWithTag("GoodCells").GetComponent<Transform>();
                }
                if (this.gameObject.tag == "NeutralCells")
                {
                    //Debug.Log("this is a Neutral cell hunting Bad cells");
                    target = GameObject.FindGameObjectWithTag("BadCells").GetComponent<Transform>();
                }
            }
*/


        }
        void CreateList()
        {
            GoodCells = GameObject.FindGameObjectsWithTag("GoodCells").ToList();
            BadCells = GameObject.FindGameObjectsWithTag("BadCells").ToList();
            NeutralCells = GameObject.FindGameObjectsWithTag("NeutralCells").ToList();
        }

        void updateList()
        {
            for (int i = 0; i < BadCells.Count; i++)
            {
                CreateList();
                if (BadCells[i] == null)
                {
                    BadCells.Remove(BadCells[i].gameObject);
                }
            }
            for (int i = 0; i < NeutralCells.Count; i++)
            {
                CreateList();
                if (NeutralCells[i] == null)
                {
                    NeutralCells.Remove(NeutralCells[i].gameObject);
                }
            }
            for (int i = 0; i < GoodCells.Count; i++)
            {
                CreateList();
                if (GoodCells[i] == null)
                {
                    GoodCells.Remove(GoodCells[i].gameObject);
                }
            }
        }

        public void ChangeTarget()
        {
            if ((flee == false) && (switching1 <= 0))
            {
                switching1++;
                switching2 = 0;
                switching3 = 0;
                FindTarget();
            }
            if ((flee == true) && (switching2 <= 0))
            {
                switching1 = 0;
                switching2++;
                switching3 = 0;
                FindTarget();
            }
            if ((lastStand == true) && (switching3 <= 0))
            {
                switching1 = 0;
                switching2 = 0;
                switching3++;
                FindTarget();
            }
        }

        void FindTarget()
        {

            float lowestDist = Mathf.Infinity;


            //starting the good cell conditions
            if (this.gameObject.tag == "GoodCells")
            {
                if(flee == false)
                {
                    //we are a good cell hunting
                    //this is for target
                    for (int i = 0; i < BadCells.Count; i++)
                    {
                        if (BadCells[i].transform.position != null)
                        {
                            float dist = Vector3.Distance(BadCells[i].transform.position, transform.position);
                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (BadCells[i].GetComponent<Transform>() != null)
                                {
                                    target = BadCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            BadCells.Remove(BadCells[i]); // add this to all the other ones
                        }
                    }
                }
                if (flee == true)
                {
                    //we are a good cell running away
                    //this is for target2Flee
                    for (int i = 0; i < GoodCells.Count; i++)
                    {
                        if (GoodCells[i].transform.position != null)
                        { 
                            float dist = Vector3.Distance(GoodCells[i].transform.position, transform.position);
                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (GoodCells[i].GetComponent<Transform>() != null)
                                {
                                    target2Flee = GoodCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            GoodCells.Remove(GoodCells[i]); // add this to all the other ones
                        }
                    }
                }
                if (lastStand == true)
                {
                    //we are a good cell hunting Neutral Cells
                    //this is for target3LastStand
                    for (int i = 0; i < NeutralCells.Count; i++)
                    {
                        if (NeutralCells[i].transform.position != null)
                        {
                            float dist = Vector3.Distance(NeutralCells[i].transform.position, transform.position);
                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (NeutralCells[i].GetComponent<Transform>() != null)
                                {
                                    target3LastStand = NeutralCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            NeutralCells.Remove(NeutralCells[i]); // add this to all the other ones
                        }
                    }
                }
            }  //good cells are done


            //starting the Bad cell conditions
            if (this.gameObject.tag == "BadCells")
            {
                if (flee == false)
                {
                   
                    for (int i = 0; i < GoodCells.Count; i++)
                    {
                        if (GoodCells[i].transform.position != null)
                        {
                            float dist = Vector3.Distance(GoodCells[i].transform.position, transform.position);
                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (GoodCells[i].GetComponent<Transform>() != null)
                                {
                                    target = GoodCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            GoodCells.Remove(GoodCells[i]); // add this to all the other ones
                        }
                    }
                }
                if (flee == true)
                {
                    for (int i = 0; i < BadCells.Count; i++)
                    {
                        if (BadCells[i].transform.position != null)
                        {
                            float dist = Vector3.Distance(BadCells[i].transform.position, transform.position);
                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (BadCells[i].GetComponent<Transform>() != null)
                                {
                                    target2Flee = BadCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            BadCells.Remove(BadCells[i]); // add this to all the other ones
                        }
                    }
                }
                if (lastStand == true)
                {
                    for (int i = 0; i < NeutralCells.Count; i++)
                    {
                        if (NeutralCells[i].transform.position != null)
                        {
                            float dist = Vector3.Distance(NeutralCells[i].transform.position, transform.position);
                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (NeutralCells[i].GetComponent<Transform>() != null)
                                {
                                    target3LastStand = NeutralCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            NeutralCells.Remove(NeutralCells[i]); // add this to all the other ones
                        }
                    }
                }
            } // Bad cells are done


            //starting the Neutral cell conditions
            if (this.gameObject.tag == "NeutralCells")
            {
                if (flee == false)
                {
                    for (int i = 0; i < BadCells.Count; i++)
                    {
                        if (BadCells[i].transform.position != null)
                        {
                            float dist = Vector3.Distance(BadCells[i].transform.position, transform.position);
                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (BadCells[i].GetComponent<Transform>() != null)
                                {
                                    target = BadCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            BadCells.Remove(BadCells[i]); // add this to all the other ones
                        }
                    }
                }
                if (flee == true)
                {
                    for (int i = 0; i < NeutralCells.Count; i++)
                    {
                        if (NeutralCells[i].transform.position != null)
                        {
                            float dist = Vector3.Distance(NeutralCells[i].transform.position, transform.position);
                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (NeutralCells[i].GetComponent<Transform>() != null)
                                {
                                    target2Flee = NeutralCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            NeutralCells.Remove(NeutralCells[i]); // add this to all the other ones
                        }
                    }
                }
                if (lastStand == true)
                {
                    for (int i = 0; i < BadCells.Count; i++)
                    {
                        if (BadCells[i].transform.position != null)
                        {
                            float dist = Vector3.Distance(BadCells[i].transform.position, transform.position);

                            if (dist < lowestDist)
                            {
                                lowestDist = dist;
                                if (BadCells[i].GetComponent<Transform>() != null)
                                {
                                    target3LastStand = BadCells[i].GetComponent<Transform>();
                                }
                            }
                        }
                        else
                        {
                            BadCells.Remove(BadCells[i]); // add this to all the other ones
                        }
                    }
                }
            }
        }
    }
}