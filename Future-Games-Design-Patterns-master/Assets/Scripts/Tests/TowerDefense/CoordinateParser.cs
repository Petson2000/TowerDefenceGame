using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CoordinateParser : MonoBehaviour
{
    
}

public class MapData
{
    private TileType[,] m_MapData;

    public MapData(TileType mapData)
    {
       // m_MapData = mapData;
    }

    public IEnumerable<Vector3> WorldCoordinates(float tileScale, Vector3 worldOrigin)
    {
        List<Vector3> points = new List<Vector3>(m_MapData.Length);

        Vector3 current = worldOrigin; //First tile
        
        for (int i = 0; i < m_MapData.GetLength(0); i++)
        {
            //World origin
            float xPos = worldOrigin.x + (tileScale * i);
            current = new Vector3();   
            
            for (int j = 0; j < m_MapData.GetLength(1); j++)
            {
                float yPos = worldOrigin.y + (tileScale * j);
                
                current = new Vector3(xPos, yPos, worldOrigin.z);
                points.Add(current);
            }   
        }

        return points;

    }
}
