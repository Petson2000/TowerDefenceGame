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

    private List<GameObject> walkables = new List<GameObject>();
    
    public TextAsset textAsset;
    
    [SerializeField] private GameObject enemyPrefab;

    private List<String> lines;

    void Start()
    {
        MapReader reader = new MapReader();
        lines = reader.ReadFile(textAsset);
        Debug.Log(lines.Count);
        BuildMap();
    }

    public void BuildMap()
    {
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
                    case'9':
                        tileType = endTile;
                        break;
                    default:
                        tileType = pathTile;
                        walkables.Add(tileType);
                        break;
                }
                Instantiate(tileType, new Vector3(x, 0, z), Quaternion.identity);
                
                if (isStartTile)
                {
                    Instantiate(enemyPrefab, new Vector3(x, 1, z), Quaternion.identity);
                    isStartTile = false;
                }
            }
        }
    }

    public List<GameObject> GetWalkableTiles()
    {
        return walkables;
    }
}