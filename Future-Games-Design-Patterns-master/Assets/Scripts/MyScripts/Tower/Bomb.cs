using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Transform m_target;

    private SphereCollider m_collider;

    public float blastRadius;

    public float speed = 70f;

    [SerializeField]private float damage;

    private List<GameObject> m_enemiesInBlastRadius;

    private Vector3 m_Direction;

    private void Awake()
    {
        m_enemiesInBlastRadius = new List<GameObject>();
        m_collider = GetComponent<SphereCollider>();
        m_collider.radius = blastRadius;
    }
/// <summary>
/// Set target to passed in transform and calculate direction
/// </summary>
    public void CalculateDirection(Transform target)
    {
        m_target = target;
        m_Direction = target.position - transform.position;
    }

    private void Update()
    {
        if (m_target == null)
        {
            gameObject.SetActive(false);
        }

        float distanceThisFrame = speed * Time.deltaTime;

        if (m_Direction.magnitude <= distanceThisFrame)
        {
            //Bomb has hit something
            HitTarget();
            return;
        }
        
        transform.Translate(m_Direction.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget() //Deal damage to all enemies in the list
    {
        foreach (GameObject enemy in m_enemiesInBlastRadius)
        {
            enemy.GetComponent<EnemyBase>().TakeDamage(damage);
        }
        
        gameObject.SetActive(false);
        m_target = null;
    }

    private void OnTriggerEnter(Collider other) //Add all enemies in sphere to a list
    {
        if (other.CompareTag("Enemy"))
        {
            m_enemiesInBlastRadius.Add(other.gameObject);
            HitTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && m_enemiesInBlastRadius.Contains(other.gameObject))
        {
            m_enemiesInBlastRadius.Remove(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }

    private void OnDisable()
    {
        m_enemiesInBlastRadius.Clear();
    }
}
