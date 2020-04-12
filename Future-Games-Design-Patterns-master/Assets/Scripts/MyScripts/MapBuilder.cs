using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private GameObject m_pathTile;
    [SerializeField] private GameObject m_towerOne;
    [SerializeField] private GameObject m_obstacleTile;
    [SerializeField] private GameObject m_towerTwo;
    [SerializeField] private GameObject m_startTile;
    [SerializeField] private GameObject m_endTile; //Goal
    private const int k_cellSize = 2;

    private Spawner spawner;
    
    private bool m_isStartTile;
    private bool m_isEndTile;
    private bool m_isWalkable;

    [System.NonSerialized]public Vector3 m_startPos;
    [System.NonSerialized]public Vector3 endPos;

    [System.NonSerialized]public List<Vector3> walkables = new List<Vector3>();

    public MapReader m_mapReader;
    
    [System.NonSerialized]public bool finishedBuilding = false;
    
    private List<String> m_lines;

    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        m_mapReader.ReadFile();
        BuildMap();
    }

    
    /// <summary>
    /// Gets Map data from map reader and converts it to gameObjects, then it places them on the map.
    /// </summary>
    public void BuildMap()
    {
        m_lines = m_mapReader.MapData; //read Map data list from mapreader

        for (int lineIndex = m_lines.Count - 1, rowIndex = 0; lineIndex >= 0; lineIndex--, rowIndex++)
        {
            string line = m_lines[lineIndex];

            for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
            {
                char item = line[columnIndex];

                float z = rowIndex * k_cellSize;
                float x = columnIndex * k_cellSize;

                GameObject tileType;

                switch (item)
                {
                    case '1':
                        tileType = m_obstacleTile;
                        break;
                    case '2':
                        tileType = m_towerOne;
                        break;
                    case '3':
                        tileType = m_towerTwo;
                        break;
                    case '8':
                        tileType = m_startTile;
                        m_isStartTile = true;
                        break;
                    case '9':
                        tileType = m_endTile;
                        m_isEndTile = true;
                        break;
                    default:
                        tileType = m_pathTile;
                        m_isWalkable = true;
                        break;
                }

                Instantiate(tileType, new Vector3(x, 0, z), Quaternion.identity);
                
                
                if (m_isWalkable)
                {
                    walkables.Add(new Vector3(x, 0, z));
                    m_isWalkable = false;
                }
                
                if (m_isStartTile)
                {
                    m_startPos = new Vector3(x, 0, z);
                    walkables.Add(m_startPos);
                    m_isStartTile = false;
                }

                if (m_isEndTile)
                {
                    endPos = new Vector3(x, 0, z);
                    walkables.Add(endPos);
                    m_isEndTile = false;
                }
            }

        }

        finishedBuilding = true;
        spawner.StartCoroutine(spawner.Spawn(m_startPos));
    }

    /// <summary>
    /// Returns the list of all walkable tiles
    /// </summary>
    public List<Vector3> GetWalkableTiles()
    {
        return walkables;
    }
}