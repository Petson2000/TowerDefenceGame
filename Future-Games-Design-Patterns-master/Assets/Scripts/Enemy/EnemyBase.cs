using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase
{
    private Vector3 targetPos;

    private GetPath path = null;

    public void DealDamage(float damage, float currentHealth)
    {
        currentHealth -= damage;
    }

    public void FollowPath(List<Vector3> path)
    {
        
    }
}
