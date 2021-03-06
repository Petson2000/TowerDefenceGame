﻿using Tools;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform target;
    public float range = 5f;

    public string enemyTag;
    
    private Spawner m_enemySpawner;

    private MapBuilder m_builder = null;

    [Tooltip("Part of turret to rotate")] public Transform towerHead;

    public float fireRate = 1f;

    private GameObjectPool m_bulletPool;

    private float m_fireCountDown = 0f;
    
    [SerializeField]private GameObject bullet;
    public Transform firePoint;
    
    private void Start()
    {
        m_builder = FindObjectOfType<MapBuilder>();
        
        m_bulletPool = new GameObjectPool(5, bullet, 2, new GameObject("Bullet Parent").transform);
        InvokeRepeating("UpdateTarget", 0.1f, 0.1f );
    }
    
    private void UpdateTarget()
    {
        if (!m_builder.finishedBuilding)
        {
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Get all enemies
        
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies) //Make sure we target the closest enemy
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance) 
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position; //Rotate to face target
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        towerHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (m_fireCountDown <= 0f)
        {
            Shoot();
            m_fireCountDown = 1f / fireRate;
        }

        m_fireCountDown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletObj = m_bulletPool.Rent(false);
        Bomb bomb = bulletObj.GetComponent<Bomb>();
        bulletObj.transform.position = firePoint.transform.position;

           if (bomb != null)
           {
               bomb.CalculateDirection(target);
           }
           bulletObj.SetActive(true);
    }
}
