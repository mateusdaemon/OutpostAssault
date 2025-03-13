using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform baseTarget;
    [SerializeField] private Collider spawnArea;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private bool activeSpawn = false;

    private IEnumerator SpawnEnemies()
    {
        while (activeSpawn)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (!activeSpawn) break; 
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnArea == null) return;

        Vector3 spawnPosition = GetRandomSpawnPosition();
        Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Enemy enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemyInstance.SetBaseTarget(baseTarget);
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

    public void StopSpawn()
    {
        if (!activeSpawn) return;
        activeSpawn = false;
        StopCoroutine(SpawnEnemies());
    }

    public void ActivateSpawn()
    {
        if (activeSpawn) return;
        activeSpawn = true;
        StartCoroutine(SpawnEnemies());
    }

    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }

    internal void SetSpawnIntervalByLevel(int level)
    {
        float newInterval = Mathf.Max(2f * (1f - (level / 10f)), 0.5f);
        spawnInterval = newInterval;
    }
}
