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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == baseTarget.gameObject)
        {
            GameManager.Instance.EnemyReachBase();
            Destroy(gameObject);
        }
    }
}
