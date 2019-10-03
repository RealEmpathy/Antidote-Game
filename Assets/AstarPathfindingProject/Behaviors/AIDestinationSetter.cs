using UnityEngine;
using System.Collections;

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
        public bool flee = false;
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

        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
        {
            //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE
            /* if(flee == false)
             {
                 if (target != null && ai != null) ai.destination = target.position;
             }
             //enable target2Flee for the new target to take place
             if (flee == true)
             {
                 if (target2Flee != null && ai != null) ai.destination = target2Flee.position;
             }*/
            //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE //ORIGINAL CODE HERE 




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
    }
}