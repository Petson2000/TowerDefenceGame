using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTester : MonoBehaviour
{
    public TextAsset textAsset;
    void Start()
    {
        MapReader reader = new MapReader();
        reader.ReadFile(textAsset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
