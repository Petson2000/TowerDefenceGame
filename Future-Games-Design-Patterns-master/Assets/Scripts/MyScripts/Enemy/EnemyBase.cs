using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float speed;
    
    private GetPath pathGetter = null;

    private List<Vector3> path;
    private int current = 0;

    public float maxHealth;

    private float currentHealth;
    
    private Vector3 targetPos;

    public void Start(float MaxHealth)
    {
        maxHealth = MaxHealth;
        currentHealth = maxHealth;
        pathGetter = GetComponent<GetPath>();
        path = pathGetter.CalculatePath();
    }

    public void Move()
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
    
    public void Die()
    {
        gameObject.SetActive(false);
        current = 0;
    }
    
    public void OnEnable()
    {
        pathGetter = GetComponent<GetPath>();
        currentHealth = maxHealth;
        path = pathGetter.CalculatePath();
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }
}
