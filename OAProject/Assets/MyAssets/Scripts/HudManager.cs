using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }

    [Header("Gameplay UI")]
    [SerializeField] GameObject gameplayPanel;
    [SerializeField] private TextMeshProUGUI lifeAmount;

    [Header("Upgrade Hability")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI playerLvlTxt;
    [SerializeField] private TextMeshProUGUI xpAmountTxt;
    [SerializeField] private TextMeshProUGUI ptsAmountTxt;
    [SerializeField] private TextMeshProUGUI atkSpeedTxt;
    [SerializeField] private TextMeshProUGUI atkDamageTxt;
    [SerializeField] private TextMeshProUGUI specialTxt;

    [Header("Upgrade Hability Buttons")]
    [SerializeField] private Button attackSpeedButton;
    [SerializeField] private Button bulletDamageButton;
    [SerializeField] private Button specialDamageButton;

    [Header("Hability Buttons")]
    [SerializeField] private Button specialAttackBtn;

    [Header("Filled Images")]
    [SerializeField] private Image expBar;
    [SerializeField] private Image lifeBar;

    [Header("Gameover UI")]
    [SerializeField] GameObject gameoverPanel;

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

    public void SetBaseLife(float baseLives)
    {
        lifeAmount.text = baseLives.ToString();
        lifeBar.fillAmount = baseLives / GameManager.Instance.BaseMaxLife();
    }

    public void GameOver()
    {
        gameplayPanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    public void ResetGame()
    {
        gameplayPanel.SetActive(true);
        gameoverPanel.SetActive(false);
    }

    public void ShowUpgradeUI()
    {
        upgradePanel.SetActive(true);
        CheckEnableUpgrades();
    }

    private void CheckEnableUpgrades()
    {
        bulletDamageButton.gameObject.SetActive(true);
        attackSpeedButton.gameObject.SetActive(true);
        specialDamageButton.gameObject.SetActive(true);

        if (GameManager.Instance.playerStats.bulletDamage >= GameManager.Instance.playerStats.maxBulletDamage)
        {
            bulletDamageButton.gameObject.SetActive(false);
        }

        if (GameManager.Instance.playerStats.attackSpeed >= GameManager.Instance.playerStats.maxAttackSpeed)
        {
            attackSpeedButton.gameObject.SetActive(false);
        }

        if (GameManager.Instance.playerStats.specialDuration >= GameManager.Instance.playerStats.maxSpecialDuration)
        {
            specialDamageButton.gameObject.SetActive(false);
        }
    }

    public void HideUpgradeUI()
    {
        upgradePanel.SetActive(false);
    }

    public void UpdateXpAmount(float amount, float maxXp)
    {
        xpAmountTxt.text = amount.ToString() + "/" + maxXp.ToString();
        expBar.fillAmount = amount / maxXp;
    }

    public void UpdateAtkSpeed(float amount)
    {
        atkSpeedTxt.text = "Atk Spd: " + amount.ToString();
    }

    public void UpdateAtkDamage(float amount)
    {
        atkDamageTxt.text = "Atk Dmg: " + amount.ToString();
    }

    public void UpdateSpecialDuration(float amount)
    {
        specialTxt.text = "Special Duration: " + amount.ToString();
    }

    public void UpdatePointsAvailable(int upgradePoints)
    {
        ptsAmountTxt.text = "+" + upgradePoints.ToString();
    }

    public void UpdatePlayerLevel(int level)
    {
        playerLvlTxt.text = "LEVEL " + level.ToString();
    }

    public void SetSpecialButton(bool enable)
    {
        specialAttackBtn.enabled = enable;

        if (!enable)
        {
            specialAttackBtn.GetComponent<Image>().fillAmount = 0;
        }        
    }

    public void SetSpecialBtnAmount(float amount)
    {
        specialAttackBtn.GetComponent<Image>().fillAmount += amount;
    }
}
