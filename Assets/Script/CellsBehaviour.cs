using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class CellsBehaviour : MonoBehaviour
{

    //public Transform target;
    private Transform target;

    public Transform CellPrefab;
    private float i, x, y;


    public float speed = 200f;
    public float nextWayPointDistance = 3f;


    Path path;

    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 2);

        if (this.gameObject.tag == "GoodCells")
        {
            Debug.Log("this is a Good cell hunting Bad cells");
            target = GameObject.FindGameObjectWithTag("BadCells").GetComponent<Transform>();
        }

        if (this.gameObject.tag == "BadCells")
        {
            Debug.Log("this is a Bad cell hunting Neutral cells");
            target = GameObject.FindGameObjectWithTag("NeutralCells").GetComponent<Transform>();
        }
        if (this.gameObject.tag == "NeutralCells")
        {
            Debug.Log("this is a Neutral cell hunting Bad cells");
            target = GameObject.FindGameObjectWithTag("BadCells").GetComponent<Transform>();
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {

            if (this.gameObject.tag == "GoodCells")
            {
                Debug.Log("this is a Good cell hunting Bad cells");
                target = GameObject.FindGameObjectWithTag("BadCells").GetComponent<Transform>();
            }

            if (this.gameObject.tag == "BadCells")
            {
                Debug.Log("this is a Bad cell hunting Neutral cells");
                target = GameObject.FindGameObjectWithTag("NeutralCells").GetComponent<Transform>();
            }
            if (this.gameObject.tag == "NeutralCells")
            {
                Debug.Log("this is a Neutral cell hunting Bad cells");
                target = GameObject.FindGameObjectWithTag("BadCells").GetComponent<Transform>();
            }
        }
        seeker.StartPath(rb.position, target.position, OnPathComplete);

    }

    /*void updateTarget()
    {
        if (seeker.IsDone())
        {   
            target = GameObject.FindGameObjectWithTag("NeutralCells").GetComponent<Transform>();
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }*/

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;
        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position.normalized, path.vectorPath[currentWayPoint]);
        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (force.x >= 0.01f && force.y >= 0.01f)
        {
            transform.Rotate( new Vector3(0f,0f,-45f));
        }
        else if (force.x >= 0.01f && force.y >= -0.01f)
        {
            transform.Rotate(new Vector3(0f, 0f, -147f));
        }
        else if (force.x >= -0.01f && force.y >= 0.01f)
        {
            transform.Rotate(new Vector3(0f, 0f, 45f));
        }
        else if (force.x >= -0.01f && force.y >= -0.01f)
        {
            transform.Rotate(new Vector3(0f, 0f, 147f));
        }
    }



}
