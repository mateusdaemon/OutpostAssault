using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private Bullet meteorPrefab;
    [SerializeField] private Collider spawnArea;
    [SerializeField] private float damage = 10f;
    private bool rainMeteor = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerEvents.OnSpecial += StartMeteorRain;
    }

    private void StartMeteorRain()
    {
        rainMeteor = true;
        StartCoroutine(MeteorRain());
        Invoke(nameof(StopMeteorRain), GameManager.Instance.playerStats.specialDuration);
    }

    private IEnumerator MeteorRain()
    {
        while (rainMeteor)
        {
            yield return new WaitForSeconds(0.2f);
            if (!rainMeteor)
                break;
            SpawnMeteor();
        }
    }

    private void SpawnMeteor()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Bullet meteor = Instantiate(meteorPrefab, spawnPosition, meteorPrefab.transform.rotation);
        meteor.SetDamage(damage);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Bounds bounds = spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.center.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void StopMeteorRain()
    {
        StopCoroutine(MeteorRain());
        rainMeteor = false;
    }

    private void OnDisable()
    {
        PlayerEvents.OnSpecial -= StartMeteorRain;
    }
}
