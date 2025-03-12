using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunPosition;
    [SerializeField] private float shootInterval;
    [SerializeField] private float shootSpeed;
    private bool canShoot = true;

    public void Shoot(Vector3 direction)
    {
        if (canShoot)
        {
            canShoot = false;
            GameObject bullet = Instantiate(bulletPrefab, gunPosition.position, bulletPrefab.transform.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * shootSpeed;
            PlayerEvents.TriggerShoot();
            Invoke("RestoreShoot", shootInterval);
        }
    }

    private void RestoreShoot()
    {
        canShoot = true;
    }

    public void SetShootInterval(float interval)
    {
        shootInterval = interval;
    }
}
