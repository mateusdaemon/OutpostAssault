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
        else
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }

    public override void Die()
    {
        GameManager.Instance.AddXP(xpReward);

        AudioManager.Instance.PlaySFXOneShot(dieSound);

        ParticleSystem particle = Instantiate(dieParticle, transform.position, Quaternion.identity);
        particle.Play();

        Destroy(gameObject);
    }
}
