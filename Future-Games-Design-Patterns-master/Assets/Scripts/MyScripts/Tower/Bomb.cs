using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Transform target;

    private SphereCollider collider;

    public float blastRadius;

    public float speed = 70f;

    [SerializeField]private float damage;

    private List<GameObject> enemiesInBlastRadius;

    private Vector3 dir;

    private void Awake()
    {
        enemiesInBlastRadius = new List<GameObject>();
        collider = GetComponent<SphereCollider>();
        collider.radius = blastRadius;
    }

    public void Fire(Transform target)
    {
        this.target = target;
        dir = target.position - transform.position;
    }

    void Update()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
        }

        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            //Bomb has hit something
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget() //Deal damage to all enemies in the list
    {
        foreach (GameObject enemy in enemiesInBlastRadius)
        {
            enemy.GetComponent<EnemyBase>().TakeDamage(damage);
        }
        
        gameObject.SetActive(false);
        target = null;
    }

    private void OnTriggerEnter(Collider other) //Add all enemies in sphere to a list
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInBlastRadius.Add(other.gameObject);
            HitTarget();
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
