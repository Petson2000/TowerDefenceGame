public class Boxymon : EnemyBase
{
    public float health;
    private void Start()
    {
        base.Start(health);
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
        base.OnDie();
    }
}
