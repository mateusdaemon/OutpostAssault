using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        PlayerEvents.OnMove += SetSpeedParam;
        PlayerEvents.OnShoot += SetShotTrigger;
    }

    private void OnDisable()
    {
        PlayerEvents.OnMove -= SetSpeedParam;
        PlayerEvents.OnShoot -= SetShotTrigger;
    }

    private void SetSpeedParam(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    private void SetShotTrigger()
    {
        animator.SetTrigger("Shoot");
    }
}
