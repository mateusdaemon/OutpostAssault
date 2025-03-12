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
        Vector3 direction = (baseTarget.position - transform.position).normalized;
        Vector3 zigzag = transform.right * Mathf.Sin(Time.time * zigzagSpeed) * zigzagAmount;
        transform.position += (direction + zigzag) * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == baseTarget.gameObject)
        {
            GameManager.Instance.EnemyReachBase();
            Destroy(gameObject);
        }
    }
}
