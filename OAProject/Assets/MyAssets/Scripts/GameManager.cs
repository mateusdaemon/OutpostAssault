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
        // if not in menu set basic stats for gameplay testing
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            HudManager.Instance.SetBaseLife(baseLives);
            playerReference = FindFirstObjectByType<Player>();
            enemySpawner = FindFirstObjectByType<EnemySpawner>();
            enemySpawner.ActivateSpawn();   
            playerSpawnPosition = GameObject.FindGameObjectWithTag("PlayerSpawn").GetComponent<Transform>();
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
        HudManager.Instance.SetMenuEnable(false);
        HudManager.Instance.ResetGame();
        HudManager.Instance.SetBaseLife(baseLives);
        playerReference = FindFirstObjectByType<Player>();
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        enemySpawner.ActivateSpawn();
        playerSpawnPosition = GameObject.FindGameObjectWithTag("PlayerSpawn").GetComponent<Transform>();
    }

    public void ReloadGame()
    {
        baseLives = 5;
        upgradePoints = 0;
        playerStats.ResetPlayerStats();

        HudManager.Instance.SetBaseLife(baseLives);
        HudManager.Instance.ResetGame();
        HudManager.Instance.UpdateXpAmount(playerStats.currentXP, playerStats.xpToNextLevel);
        HudManager.Instance.UpdatePlayerLevel(playerStats.level);
        HudManager.Instance.HideUpgradeUI();
        HudManager.Instance.UpdatePointsAvailable(upgradePoints);

        GameObject.FindGameObjectWithTag("MeteorSpawner").GetComponent<MeteorSpawner>().StopMeteorRain();
        CancelInvoke(nameof(SpecialAttackStop));

        playerReference.transform.position = playerSpawnPosition.position;
        playerReference.gameObject.SetActive(true);
        enemySpawner.ActivateSpawn();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
        HudManager.Instance.SetMenuEnable(true);
    }

    public void PlayerLevelUp()
    {
        upgradePoints++;

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
