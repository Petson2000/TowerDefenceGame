using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI;
using UnityEngine;

public class Move : MonoBehaviour
{
    private MapBuilder builder = null;
    private Dijkstra pathFinder = null;
    List<Vector3> walkableTiles = new List<Vector3>(); 
    List<Vector2Int> walkablePositions = new List<Vector2Int>();

    public GameObject testObj;
    
    public GameObject Enemy;
    public float speed;

    private float delay = 2f;

    public IEnumerable<Vector2Int> path;
    private GameObject endTile;
    private GameObject startTile;

    private Vector2Int endTilePos;
    private Vector2Int startTilePos;
    
    void Start()
    {
        Enemy = this.gameObject;

        builder = FindObjectOfType<MapBuilder>();
        ConvertToVec2Int();
    }

    private void ConvertToVec2Int()
    {
        if (builder.finishedBuilding)
        {
            walkableTiles = builder.GetWalkableTiles();
        }
        
        foreach (Vector3 V in walkableTiles)
        {
            walkablePositions.Add(new
                Vector2Int((int)V.x / 2, (int)V.z / 2));
        }
        
        pathFinder = new Dijkstra(walkablePositions);
        MoveCharacter();
    }

    void MoveCharacter()
    {
        //Split these two vector2Ints by 2 later
        startTilePos = new Vector2Int((int)builder.startPos.x / 2, (int) builder.startPos.z) / 2;
        endTilePos = new Vector2Int((int) builder.endPos.x / 2, (int) builder.endPos.z) / 2;
        
        Vector3 targetPos;
        path = pathFinder.FindPath(startTilePos, endTilePos);

        if (path != null)
        {
            //  Debug.Log(endTilePos);
            //Debug.Log("Nodes in path: " + path.Count());
            for (int i = 0; i < path.Count(); i++)
            {
                Debug.Log(path.ElementAt(i));

                targetPos = new Vector3(path.ElementAt(i).x * 2, 0, path.ElementAt(i).y * 2);
                
                Instantiate(testObj, targetPos, Quaternion.identity);
                
               // transform.position = targetPos;
            }
        }
    }
}
