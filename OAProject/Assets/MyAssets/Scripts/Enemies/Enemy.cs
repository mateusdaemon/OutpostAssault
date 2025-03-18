using UnityEngine;

public abstract class Enemy : MonoBehaviour, IRunBase, ITakeDamage
{
    [SerializeField] protected float life;
    [SerializeField] protected float speed;
    [SerializeField] protected float xpReward;
    protected Transform baseTarget;

    [Header("Sounds")]
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected AudioClip dieSound;

    [Header("Particles")]
    [SerializeField] protected ParticleSystem dieParticle;


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
