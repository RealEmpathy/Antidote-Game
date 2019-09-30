using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform spawPos;
    public GameObject BadcellPrefab;
    public GameObject GoodcellPrefab;
    public GameObject NeutralcellPrefab;

    public int numberOfGoodCells;
    public int numberOfNeutralCells;
    public int numberOfBadCells;

    public Vector2 center;
    public Vector2 size;

    private bool SpawGood;
    private bool SpawNeutral;
    private bool SpawBad;

    private int i;
    private bool Reproduction;


    void Start()
    {
        for (i = 0; i < numberOfGoodCells; i++)
        {
            SpawGood = true;
            SpawNeutral = false;
            SpawBad = false;
            //Debug.Log("Spawn Cells being called");
            SpawnCells();
        }
        for (i = 0; i < numberOfNeutralCells; i++)
        {
            SpawGood = false;
            SpawNeutral = true;
            SpawBad = false;
            //Debug.Log("Spawn Cells being called");
            SpawnCells();
        }
        for (i = 0; i < numberOfBadCells; i++)
        {
            SpawGood = false;
            SpawNeutral = false;
            SpawBad = true;
            //Debug.Log("Spawn Cells being called");
            SpawnCells();
        }

    }

    void onDrawGizmoSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    public void SpawnCells()
    {
        if(SpawGood == true)
        {
            Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(GoodcellPrefab, pos, Quaternion.identity);
        }
        if (SpawNeutral == true)
        {
            Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(NeutralcellPrefab, pos, Quaternion.identity);
        }
        if (SpawBad == true)
        {
            Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(BadcellPrefab, pos, Quaternion.identity);
        }
    }


}


