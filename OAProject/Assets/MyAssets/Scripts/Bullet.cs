using UnityEngine;

public class Bullet : MonoBehaviour
{
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
            Debug.Log("Hit enemy");
            enemy.TakeDamage(3);
        }
    }
}
