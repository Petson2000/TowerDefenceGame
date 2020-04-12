﻿using System.Collections.Generic;
using System.Linq;
using AI;
using UnityEngine;

public class GetPath : MonoBehaviour
{
    private MapBuilder builder = null;
    private Dijkstra pathFinder;
    private List<Vector3> walkableTiles = new List<Vector3>();
    private List<Vector2Int> walkablePositions = new List<Vector2Int>();

    private IEnumerable<Vector2Int> path;
    private GameObject endTile;
    private GameObject startTile;

    private List<Vector3> convertedPath;

    private int currentPoint;

    private Vector2Int endTilePos;
    private Vector2Int startTilePos;

    private void Awake()
    {
        builder = FindObjectOfType<MapBuilder>();
    }

    public List<Vector3> CalculatePath()
    {
        if (builder.finishedBuilding)
        {
            convertedPath = new List<Vector3>();
            walkableTiles = builder.GetWalkableTiles();
        }

        foreach (Vector3 V in walkableTiles)
        {
            walkablePositions.Add(new Vector2Int((int) V.x / 2, (int) V.z / 2));
        }
        
        startTilePos = new Vector2Int((int) builder.startPos.x / 2, (int) builder.startPos.z / 2);
        endTilePos = new Vector2Int((int) builder.endPos.x / 2, (int) builder.endPos.z / 2);
        
        pathFinder = new Dijkstra(walkablePositions);
        path = pathFinder.FindPath(startTilePos, endTilePos).ToList();
        
        //Convert to Vector3
        foreach (Vector2Int vec2Int in path)
        {
            convertedPath.Add(new Vector3(vec2Int.x * 2,1 ,vec2Int.y * 2));
        }

        return convertedPath;
    }
}