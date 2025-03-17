using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform gunPosition;
    [SerializeField] private float shootInterval;
    [SerializeField] private float shootSpeed;
    private bool canShoot = true;
    private float wantsShoot = 0;

    public void Shoot(Vector3 direction, float attack)
    {
        wantsShoot = attack;

        if (canShoot && wantsShoot != 0)
        {
            canShoot = false;
            Bullet bullet = Instantiate(bulletPrefab, gunPosition.position, bulletPrefab.transform.rotation);
            bullet.SetDamage(GameManager.Instance.playerStats.bulletDamage);
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * shootSpeed;
            PlayerEvents.TriggerShoot(true);
            Invoke("RestoreShoot", shootInterval);
        }
    }

    public void StopShoot()
    {
        PlayerEvents.TriggerShoot(false);
    }

    private void RestoreShoot()
    {
        if (wantsShoot == 0)
        {
            StopShoot();
        }
        canShoot = true;
    }

    public void SetShootInterval(float interval)
    {
        shootInterval = 1 / interval;
    }

    public bool CanShoot()
    {
        return canShoot;
    }
}
