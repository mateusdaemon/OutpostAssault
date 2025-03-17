using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        PlayerEvents.OnMove += SetMoveParams;
        PlayerEvents.OnShoot += SetShotTrigger;
    }

    private void OnDisable()
    {
        PlayerEvents.OnMove -= SetMoveParams;
        PlayerEvents.OnShoot -= SetShotTrigger;
    }

    private void SetMoveParams(float moveX, float moveY)
    {
        animator.SetFloat("Right", moveX);
        animator.SetFloat("Forward", moveY);
    }

    private void SetShotTrigger(bool shoot)
    {
        animator.SetBool("Shoot", shoot);
    }
}
