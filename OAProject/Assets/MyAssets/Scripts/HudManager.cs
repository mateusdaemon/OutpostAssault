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
    [SerializeField] private TextMeshProUGUI xpAmountTxt;
    [SerializeField] private TextMeshProUGUI ptsAmountTxt;
    [SerializeField] private TextMeshProUGUI atkSpeedTxt;
    [SerializeField] private TextMeshProUGUI atkDamageTxt;
    [SerializeField] private TextMeshProUGUI specialTxt;
    [SerializeField] private Button attackSpeedButton;
    [SerializeField] private Button bulletDamageButton;
    [SerializeField] private Button specialDamageButton;

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

    private void Start()
    {
        attackSpeedButton.onClick.AddListener(() => GameManager.Instance.UpgradeAttribute("AttackSpeed"));
        bulletDamageButton.onClick.AddListener(() => GameManager.Instance.UpgradeAttribute("BulletDamage"));
        specialDamageButton.onClick.AddListener(() => GameManager.Instance.UpgradeAttribute("SpecialDamage"));
    }

    public void SetBaseLife(int baseLives)
    {
        lifeAmount.text = baseLives.ToString();
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
    }

    public void HideUpgradeUI()
    {
        upgradePanel.SetActive(false);
    }

    public void UpdateXpAmount(float amount, float maxXp)
    {
        xpAmountTxt.text = "XP: " + amount.ToString() + "/" + maxXp.ToString();
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

    internal void UpdatePointsAvailable(int upgradePoints)
    {
        xpAmountTxt.text += upgradePoints.ToString();
    }
}
