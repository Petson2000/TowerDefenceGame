using System;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private GameObject pathTile;
    [SerializeField] private GameObject towerOne;
    [SerializeField] private GameObject obstacleTile;
    [SerializeField] private GameObject towerTwo;

    [SerializeField] private int cellSize = 2;

    public TextAsset textAsset;

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
        for (int lineIndex = lines.Count - 1; lineIndex >= 0; lineIndex--)
        {
            string line = lines[lineIndex];

            for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
            {
                char item = line[columnIndex];

                float z = lineIndex * cellSize;
                float x = columnIndex * cellSize;
                
                GameObject objectType;
                switch (item)
                {
                    case '1':
                        objectType = obstacleTile;
                        break;
                    
                    case '2':
                        objectType = towerOne;
                        break;
                    
                    case '3':
                        objectType = towerTwo;
                        break;
                    
                    default:
                        objectType = pathTile;
                        break;
                }
                
                Instantiate(objectType, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
}