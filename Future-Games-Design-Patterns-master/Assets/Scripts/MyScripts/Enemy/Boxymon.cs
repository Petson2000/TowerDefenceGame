using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Boxymon : EnemyBase
{
    public float speed;
    
    private Vector3 targetWaypoint;
    
    private List<Vector3> path;
    private int current = 0;

    public float maxHealth;

    private float currentHealth;
    
    private Vector3 targetPos;
    
    private void Start()
    {
        base.Start(maxHealth);
    }

    private void OnEnable()
    {
        base.OnEnable();
    }

    private void Update()
    {
        base.Move();
    }

    private void Move()
    {
        base.Move();
    }

    public void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    private void Die()
    {
        base.Die();
    }
}
