using UnityEngine;

public abstract class Enemy : MonoBehaviour, IRunBase
{
    [SerializeField] protected float life;
    [SerializeField] protected float speed;
    protected Transform baseTarget;

    public void SetBaseTarget(Transform target)
    {
        baseTarget = target;
    }

    public abstract void RunToBase();
}
