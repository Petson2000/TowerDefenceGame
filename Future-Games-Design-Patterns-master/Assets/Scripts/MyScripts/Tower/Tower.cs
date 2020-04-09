using Tools;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform target;
    public float range = 5f;

    public string enemyTag;
    
    private Spawner enemySpawner;

    private MapBuilder builder = null;

    [Tooltip("Part of turret to rotate")] public Transform towerHead;

    public float fireRate = 1f;

    private GameObjectPool bulletPool;

    private float fireCountDown = 0f;
    
    [SerializeField]private GameObject bullet;
    public Transform firePoint;
    
    void Start()
    {
        builder = FindObjectOfType<MapBuilder>();
        
        bulletPool = new GameObjectPool(30, bullet, 10, new GameObject("Bullet parent").transform);
        
        InvokeRepeating("UpdateTarget", 0.5f, 0.5f );
    }
    
    void UpdateTarget()
    {
        if (!builder.finishedBuilding)
        {
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Refactor later, very expensive
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
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

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        towerHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletObj = bulletPool.Rent(false);
           // (GameObject)Instantiate(bullet, firePoint.position, Quaternion.identity);
           bulletObj.transform.position = firePoint.transform.position;
           bulletObj.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
