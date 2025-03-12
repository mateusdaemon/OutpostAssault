using UnityEngine;

public abstract class Enemy : MonoBehaviour, IRunBase, ITakeDamage
{
    [SerializeField] protected float life;
    [SerializeField] protected float speed;
    protected Transform baseTarget;

    public void SetBaseTarget(Transform target)
    {
        baseTarget = target;
    }

    public abstract void RunToBase();
    public abstract void TakeDamage(float damage);
    public abstract void Die();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BaseTarget"))
        {
            GameManager.Instance.EnemyReachBase();
            Destroy(gameObject);
        }
    }
}
