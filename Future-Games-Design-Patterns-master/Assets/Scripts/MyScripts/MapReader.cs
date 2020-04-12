using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Map Reader", menuName = "ScriptableObjects/Map reader", order = 2)]
public class MapReader : ScriptableObject
{
    private string fileData;

    [System.NonSerialized]public List<String> MapData = new List<string>();
    [System.NonSerialized]public List<int> spawnWaves = new List<int>();

    public TextAsset Map;

    public void ReadFile()
    {
        string path = AssetDatabase.GetAssetPath(Map);

        using(StreamReader reader = new StreamReader(path))
        {
            bool readingMapData = true;
            int spawnWaveIndex = 1;

            do
            {
                string line = reader.ReadLine();

                if (line == "#")
                {
                    readingMapData = false;
                }

                else if (readingMapData)
                {
                    MapData.Add(line);
                }

                else
                {
                    string[] spawnNumbers = line.Split(' ');
                    if (spawnNumbers.Length == 2)
                    {
                        //Currently only one enemy type
                        spawnWaves.Add(int.Parse(spawnNumbers[0]) + int.Parse(spawnNumbers[1]));
                    }
                }
                
            } while (!reader.EndOfStream);
        }
    }
}
