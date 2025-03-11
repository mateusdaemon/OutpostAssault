using System.Collections;
using UnityEngine;

public class EnemyFerret : Enemy
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
}
