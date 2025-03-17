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

            Bullet bullet = Instantiate(bulletPrefab, gunPosition.position, Quaternion.identity);

            Vector3 bulletDir = direction.normalized;
            bullet.transform.rotation = Quaternion.LookRotation(bulletDir) * Quaternion.Euler(90, 0, 0);
            bullet.SetDamage(GameManager.Instance.playerStats.bulletDamage);
            bullet.GetComponent<Rigidbody>().linearVelocity = bulletDir * shootSpeed;

            PlayerEvents.TriggerShoot();

            Invoke(nameof(RestoreShoot), shootInterval);
        }
    }

    private void RestoreShoot()
    {
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
