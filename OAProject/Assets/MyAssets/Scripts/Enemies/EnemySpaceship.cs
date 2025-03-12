using UnityEngine;

public class EnemySpaceship : Enemy
{
    void Update()
    {
        RunToBase();
    }

    public override void RunToBase()
    {
        if (baseTarget == null) return;
        transform.position = Vector3.MoveTowards(transform.position, baseTarget.position, speed * Time.deltaTime);
    }

    public override void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
