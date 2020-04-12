using System.Collections.Generic;
using System.Linq;
using AI;
using UnityEngine;

public class GetPath : MonoBehaviour
{
    private MapBuilder m_mapBuilder = null;
    private Dijkstra m_pathFinder;
    private List<Vector3> m_walkableTiles = new List<Vector3>();
    private List<Vector2Int> m_walkablePositions = new List<Vector2Int>();

    private IEnumerable<Vector2Int> m_Path;
    private GameObject m_endTile;
    private GameObject m_startTile;

    private List<Vector3> m_convertedPath;

    private int m_currentPoint;

    private Vector2Int m_endTilePos;
    private Vector2Int m_startTilePos;

    private void Awake()
    {
        m_mapBuilder = FindObjectOfType<MapBuilder>();
    }

    
    /// <summary>
    ///  Using the walkable tiles, will calculate a path for a unit using the Dijkstra algorithm
    /// </summary>
    public List<Vector3> CalculatePath()
    {
        if (m_mapBuilder.finishedBuilding)
        {
            m_convertedPath = new List<Vector3>();
            m_walkableTiles = m_mapBuilder.GetWalkableTiles();
        }

        foreach (Vector3 V in m_walkableTiles)
        {
            m_walkablePositions.Add(new Vector2Int((int) V.x / 2, (int) V.z / 2));
        }
        
        m_startTilePos = new Vector2Int((int) m_mapBuilder.m_startPos.x / 2, (int) m_mapBuilder.m_startPos.z / 2);
        m_endTilePos = new Vector2Int((int) m_mapBuilder.endPos.x / 2, (int) m_mapBuilder.endPos.z / 2);
        
        m_pathFinder = new Dijkstra(m_walkablePositions);
        m_Path = m_pathFinder.FindPath(m_startTilePos, m_endTilePos).ToList();
        
        //Convert to Vector3
        foreach (Vector2Int vec2Int in m_Path)
        {
            m_convertedPath.Add(new Vector3(vec2Int.x * 2,1 ,vec2Int.y * 2));
        }

        return m_convertedPath;
    }
}