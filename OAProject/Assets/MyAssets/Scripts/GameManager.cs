using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int baseLives = 1;
    private Player playerReference;
    private EnemySpawner enemySpawner;
    private Transform playerSpawnPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        HudManager.Instance.SetBaseLife(baseLives);
        playerReference = FindFirstObjectByType<Player>();
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        enemySpawner.ActivateSpawn();
        playerSpawnPosition = GameObject.FindGameObjectWithTag("PlayerSpawn").GetComponent<Transform>();
    }


    public void EnemyReachBase()
    {
        baseLives--;
        HudManager.Instance.SetBaseLife(baseLives);

        if (baseLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        HudManager.Instance.GameOver();
        enemySpawner.StopSpawn();
        ClearAllEnemies();
        playerReference.gameObject.SetActive(false);
    }

    private void ClearAllEnemies()
    {
        foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID))
        {
            Destroy(enemy.gameObject);
        }
    }

    public void ReloadGame()
    {
        baseLives = 5;
        HudManager.Instance.SetBaseLife(baseLives);
        HudManager.Instance.ResetGame();
        playerReference.transform.position = playerSpawnPosition.position;
        playerReference.gameObject.SetActive(true);
        enemySpawner.ActivateSpawn();
    }
}
