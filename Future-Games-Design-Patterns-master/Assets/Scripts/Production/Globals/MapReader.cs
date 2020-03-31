using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MapReader
{
    private string fileData;

    public List<string> ReadFile(TextAsset map)
    {
        string path = AssetDatabase.GetAssetPath(map);
        StreamReader reader = new StreamReader(path);

        List<String> testList = new List<string>();

        string line;

        while ((line = reader.ReadLine()) != null)
        {
            if (line.Contains("#"))
            {
                break;
            }

            testList.Add(line);
        }

        return testList;
    }
}