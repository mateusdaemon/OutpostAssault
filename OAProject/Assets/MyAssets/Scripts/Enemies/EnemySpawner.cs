using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform baseTarget;
    [SerializeField] private Collider spawnArea;
    [SerializeField] private float spawnInterval = 3f;
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
        activeSpawn = false;
        StopCoroutine(SpawnEnemies());
    }

    public void ActivateSpawn()
    {
        activeSpawn = true;
        StartCoroutine(SpawnEnemies());
    }
}
