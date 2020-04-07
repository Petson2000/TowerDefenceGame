using System;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private GameObject pathTile;
    [SerializeField] private GameObject towerOne;
    [SerializeField] private GameObject obstacleTile;
    [SerializeField] private GameObject towerTwo;
    [SerializeField] private GameObject startTile;
    [SerializeField] private GameObject endTile; //Goal

    [SerializeField] private int cellSize = 2;

    private bool isStartTile;
    private bool isEndTile;
    private bool isWalkable;

    public Vector3 startPos;
    public Vector3 endPos;

    public List<Vector3> walkables = new List<Vector3>();

    public TextAsset textAsset;

    public bool finishedBuilding = false;
    
    [SerializeField] private GameObject enemyPrefab;

    private List<String> lines;

    void Awake()
    {
        BuildMap();
    }

    public void BuildMap()
    {
        MapReader reader = new MapReader();
        lines = reader.ReadFile(textAsset);

        for (int lineIndex = lines.Count - 1, rowIndex = 0; lineIndex >= 0; lineIndex--, rowIndex++)
        {
            string line = lines[lineIndex];

            for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
            {
                char item = line[columnIndex];

                float z = rowIndex * cellSize;
                float x = columnIndex * cellSize;

                GameObject tileType;

                switch (item)
                {
                    case '1':
                        tileType = obstacleTile;
                        break;
                    case '2':
                        tileType = towerOne;
                        break;
                    case '3':
                        tileType = towerTwo;
                        break;
                    case '8':
                        tileType = startTile;
                        isStartTile = true;
                        break;
                    case '9':
                        tileType = endTile;
                        isEndTile = true;
                        break;
                    default:
                        tileType = pathTile;
                        isWalkable = true;
                        break;
                }

                Instantiate(tileType, new Vector3(x, 0, z), Quaternion.identity);
                if (isWalkable)
                {
                    walkables.Add(new Vector3(x, 0, z));
                    isWalkable = false;
                }
                //Add position here instead, otherwise 0,0,0
                if (isStartTile)
                {
                    startPos = new Vector3(x, 1, z);
                    walkables.Add(startPos);
                    Instantiate(enemyPrefab, startPos, Quaternion.identity);
                    isStartTile = false;
                }

                if (isEndTile)
                {
                    endPos = new Vector3(x, 0, z);
                    walkables.Add(endPos);
                    isEndTile = false;
                }
            }

        }

        finishedBuilding = true;
    }

    public List<Vector3> GetWalkableTiles()
    {
        return walkables;
    }
}