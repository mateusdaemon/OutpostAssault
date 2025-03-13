using UnityEngine;

public class Bullet : MonoBehaviour
{
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
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
}
