using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float baseLives = 5;
    private float baseMaxLives = 5;
    private Player playerReference;
    private EnemySpawner enemySpawner;
    private Transform playerSpawnPosition;

    public PlayerStats playerStats;

    private int upgradePoints = 0;
    private bool specialColdown = false;

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
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            LoadReferences();
        }
    }

    private void FixedUpdate()
    {
        if (specialColdown)
        {
            HudManager.Instance.SetSpecialBtnAmount(1.0f / GameManager.Instance.playerStats.specialColdown * Time.deltaTime);
        }
    }

    public void EnemyReachBase()
    {
        baseLives--;
        HudManager.Instance.SetBaseLife(baseLives);
        PlayerEvents.TriggerPlayerDmg();

        if (baseLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        HudManager.Instance.GameOver();
        enemySpawner.SetSpawnInterval(3.0f);
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

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
        SceneManager.sceneLoaded += OnGameSceneLoaded;
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            SceneManager.sceneLoaded -= OnGameSceneLoaded;
            HudManager.Instance.SetMenuEnable(false);
            ResetGameData();
            LoadReferences();
        }
    }

    private void ResetGameData()
    {
        baseLives = 5;
        upgradePoints = 0;
        playerStats.ResetPlayerStats();
    }

    private void LoadReferences()
    {
        playerReference = FindFirstObjectByType<Player>();
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        playerSpawnPosition = GameObject.FindGameObjectWithTag("PlayerSpawn")?.transform;

        if (enemySpawner != null)
        {
            enemySpawner.ActivateSpawn();
        }

        if (HudManager.Instance != null)
        {
            HudManager.Instance.ResetGame(playerStats);
        }
    }

    public void ReloadGame()
    {
        baseLives = 5;
        upgradePoints = 0;
        playerStats.ResetPlayerStats();

        HudManager.Instance.ResetGame(playerStats);

        GameObject.FindGameObjectWithTag("MeteorSpawner").GetComponent<MeteorSpawner>().StopMeteorRain();
        CancelInvoke(nameof(SpecialAttackStop));

        playerReference.transform.position = playerSpawnPosition.position;
        playerReference.UpdatePlayerAtt(playerStats);
        playerReference.gameObject.SetActive(true);
        enemySpawner.ActivateSpawn();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
        SceneManager.sceneLoaded += OnMenuLoaded;
    }

    private void OnMenuLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            SceneManager.sceneLoaded -= OnMenuLoaded;
            ResetGameData();
            playerReference = null;
            enemySpawner = null;
            playerSpawnPosition = null;
            HudManager.Instance.SetMenuEnable(true);
        }
    }

    public void PlayerLevelUp()
    {
        upgradePoints++;

        PlayerEvents.TriggerLevelUp();

        HudManager.Instance.UpdatePointsAvailable(upgradePoints);
        HudManager.Instance.ShowUpgradeUI();
        
        enemySpawner.SetSpawnIntervalByLevel(playerStats.level);
    }

    public void AddXP(float amount)
    {
        playerStats.AddXP(amount);
        HudManager.Instance.UpdateXpAmount(playerStats.currentXP, playerStats.xpToNextLevel);
    }

    public void UpgradeAttribute(string attribute)
    {
        if (upgradePoints <= 0) return;

        switch (attribute)
        {
            case "AttackSpeed":
                playerStats.attackSpeed += 1f;
                HudManager.Instance.UpdateAtkSpeed(playerStats.attackSpeed);
                break;
            case "BulletDamage":
                playerStats.bulletDamage += 3f;
                HudManager.Instance.UpdateAtkDamage(playerStats.bulletDamage);
                break;
            case "SpecialDamage":
                playerStats.specialDuration += 2f;
                HudManager.Instance.UpdateSpecialDuration(playerStats.specialDuration);
                break;
        }

        upgradePoints--;
        HudManager.Instance.UpdatePointsAvailable(upgradePoints);

        if (upgradePoints <= 0 )
        {
            HudManager.Instance.HideUpgradeUI();
        }

        playerReference.UpdatePlayerAtt(playerStats);
    }

    public float BaseMaxLife()
    {
        return baseMaxLives;
    }

    public void SpecialAttackStart()
    {
        HudManager.Instance.SetSpecialButton(false);
        PlayerEvents.TriggerSpecial();
        specialColdown = true;
        Invoke("SpecialAttackStop", playerStats.specialColdown);
    }

    public void SpecialAttackStop()
    {
        HudManager.Instance.SetSpecialButton(true);
        HudManager.Instance.SetSpecialBtnAmount(1.0f);
        specialColdown = false;
    }


}
