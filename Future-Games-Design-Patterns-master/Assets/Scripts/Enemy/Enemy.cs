using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    
    private GetPath pathGetter = null;
    private MapBuilder builder = null;

    private Vector3 targetWaypoint;
    
    private List<Vector3> path;
    private int current = 0;
    
    private Vector3 targetPos;
    
    private void Start()
    {
        pathGetter = FindObjectOfType<GetPath>();
        builder = FindObjectOfType<MapBuilder>();
        path = pathGetter.CalculatePath();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        targetPos = path[current];

        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        else
        {
            if (path[current] != path[path.Count - 1])
            {
                current++;
            }

            else
            {
                Destroy(this.gameObject);
            }
        }

    }
}
