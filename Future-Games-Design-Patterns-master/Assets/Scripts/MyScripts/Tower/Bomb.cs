using System;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Transform target;

    private SphereCollider collider;

    public float blastRadius;

    public float speed = 70f;

    private List<GameObject> enemiesInBlastRadius;

    private void Awake()
    {
        enemiesInBlastRadius = new List<GameObject>();
        collider = GetComponent<SphereCollider>();
        collider.radius = blastRadius;
    }

    public void Seek(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target == null)
        {
            this.gameObject.SetActive(false);
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            //Bomb has hit something
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Debug.Log("Hit!");

        foreach (GameObject enemy in enemiesInBlastRadius)
        {
            enemy.GetComponent<EnemyBase>().TakeDamage(20f);
        }
        
        this.gameObject.SetActive(false);
        target = null;
        //Use observer pattern?
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInBlastRadius.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && enemiesInBlastRadius.Contains(other.gameObject))
        {
            enemiesInBlastRadius.Remove(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }

    private void OnDisable()
    {
        enemiesInBlastRadius.Clear();
    }
}
