using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float speed;

    public int damage = 20;
    
    private GetPath m_pathGetter = null;

    private List<Vector3> m_path;
    
    private int m_currentWaypoint = 0;

    private float m_maxHealth;

    private float m_currentHealth;
    
    private Vector3 m_targetPos;

    private Spawner m_spawner;

    private bool m_isDead = false;

    private PlayerHealth m_player;

    public void Start(float MaxHealth)
    {
        m_spawner = FindObjectOfType<Spawner>();
        m_path = new List<Vector3>();
        m_maxHealth = MaxHealth;
        m_currentHealth = m_maxHealth;
        m_pathGetter = GetComponent<GetPath>();
        m_path = m_pathGetter.CalculatePath();
        m_player = FindObjectOfType<PlayerHealth>();
    }

    /// <summary>
    /// Move on the calculated path on dijkstra
    /// </summary>
    public void Move()
    {
        m_targetPos = m_path[m_currentWaypoint];

        if (transform.position != m_targetPos)
        {
            Vector3 dir = m_targetPos - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            transform.position = Vector3.MoveTowards(transform.position, m_targetPos, speed * Time.deltaTime);
        }

        else
        {
            if (m_path[m_currentWaypoint] != m_path[m_path.Count - 1])
            {
                m_currentWaypoint++;
            }

            else
            {
                gameObject.SetActive(false);
                m_spawner.killedEnemies++;
                m_player.TakeDamage(damage);
                m_isDead = true;
            }
        }
    }
    
    /// <summary>
    /// Called when the enemy dies
    /// </summary>
    public void OnDie()
    {
            m_currentWaypoint = 0;
            m_spawner.killedEnemies++;
            m_isDead = true;
    }
    
    public void OnEnable()
    {
        m_path = new List<Vector3>();
        m_pathGetter = GetComponent<GetPath>();
        m_currentHealth = m_maxHealth;
        m_path = m_pathGetter.CalculatePath();
        m_isDead = false;
        m_currentWaypoint = 0;
    }
    
    /// <summary>
    /// Called when the enemy takes damage
    /// </summary>
    public void TakeDamage(float damage)
    {
        m_currentHealth -= damage;
        if (!m_isDead)
        {
            if (m_currentHealth <= 0f)
            {
                gameObject.SetActive(false);
                OnDie();
            }
        }
    }
}
