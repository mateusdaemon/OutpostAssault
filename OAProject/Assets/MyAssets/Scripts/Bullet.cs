using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticle;

    private float damage = 0;

    private void Start()
    {
        Invoke("DestroyMyself", 5.0f);
    }

    private void DestroyMyself()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // stop the bullet
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            ParticleSystem particle = Instantiate(hitParticle, hitPoint, Quaternion.identity);
            particle.Play();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            ParticleSystem particle = Instantiate(hitParticle, transform.position, Quaternion.identity);
            particle.Play();
        }
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
}
