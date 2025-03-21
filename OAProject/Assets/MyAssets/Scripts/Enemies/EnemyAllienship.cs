using UnityEngine;

public class EnemyAllienship : Enemy
{
    private float zigzagAmount = 1f;
    private float zigzagSpeed = 2f;

    void Update()
    {
        RunToBase();
    }

    public override void RunToBase()
    {
        if (baseTarget == null) return;

        // Calcula a dire��o apenas nos eixos X e Z
        Vector3 direction = (baseTarget.position - transform.position);
        direction.y = 0; // Mant�m a altura fixa
        direction.Normalize();

        // Calcula o deslocamento do zig-zag
        Vector3 rightDirection = Vector3.Cross(Vector3.up, direction).normalized; // Perpendicular � dire��o principal
        Vector3 zigzag = rightDirection * Mathf.Sin(Time.time * zigzagSpeed) * zigzagAmount;

        // Move a nave
        transform.position += (direction + zigzag) * speed * Time.deltaTime;
    }

    public override void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
        } else
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
