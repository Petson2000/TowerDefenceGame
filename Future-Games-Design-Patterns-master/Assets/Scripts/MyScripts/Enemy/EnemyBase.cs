using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float speed;

    public int damage = 20;
    
    private GetPath pathGetter = null;

    private List<Vector3> path;
    private int current = 0;

    private float maxHealth;

    private float currentHealth;
    
    private Vector3 targetPos;

    private Spawner spawner;

    private bool isDead = false;

    private PlayerHealth player;

    public void Start(float MaxHealth)
    {
        spawner = FindObjectOfType<Spawner>();
        path = new List<Vector3>();
        maxHealth = MaxHealth;
        currentHealth = maxHealth;
        pathGetter = GetComponent<GetPath>();
        path = pathGetter.CalculatePath();
        player = FindObjectOfType<PlayerHealth>();
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
                gameObject.SetActive(false);
                spawner.killedEnemies++;
                player.TakeDamage(damage);
                isDead = true;
            }
        }
    }
    
    public void OnDie()
    {
            current = 0;
            spawner.killedEnemies++;
            isDead = true;
    }
    
    public void OnEnable()
    {
        path = new List<Vector3>();
        pathGetter = GetComponent<GetPath>();
        currentHealth = maxHealth;
        path = pathGetter.CalculatePath();
        isDead = false;
        current = 0;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (!isDead)
        {
            if (currentHealth <= 0f)
            {
                gameObject.SetActive(false);
                OnDie();
            }
        }
    }
}
