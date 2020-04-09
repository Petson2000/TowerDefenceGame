using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    public float speed;
    
    private GetPath pathGetter = null;

    private Vector3 targetWaypoint;
    
    private List<Vector3> path;
    private int current = 0;
    
    private Vector3 targetPos;
    
    private void Start()
    {
        pathGetter = GetComponent<GetPath>();
            
        path = pathGetter.CalculatePath();
    }

    private void Update()
    {
        Move();
    }

    private void Push()
    {
        
    }

    private void Move()
    {
        targetPos = path[current];

        if (transform.position != targetPos)
        {
            Vector3 dir = targetPos - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
                this.gameObject.SetActive(false);
            }
        }

    }
}
