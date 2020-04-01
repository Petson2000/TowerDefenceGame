using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Start()
    {
        IPathFinder pathFinder = new Dijkstra();
    }

    private void Move()
    {
        
    }
}
