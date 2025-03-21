using UnityEngine;
using UnityEngine.UI;

public class InputUiManager : MonoBehaviour
{
    [Header("Gameover Buttons")]
    public Button playAgainBtn;

    [Header("Upgrade Skills Buttons")]
    [SerializeField] private Button attackSpeedButton;
    [SerializeField] private Button bulletDamageButton;
    [SerializeField] private Button specialDamageButton;

    [Header("Special Attack Button")]
    [SerializeField] private Button specialAttackBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupButtons();
    }

    public void SetupButtons()
    {
        // Menu Principal
        playAgainBtn.onClick.AddListener(() => GameManager.Instance.ReloadGame());

        // Upgrade skills
        attackSpeedButton.onClick.AddListener(() => GameManager.Instance.UpgradeAttribute("AttackSpeed"));
        bulletDamageButton.onClick.AddListener(() => GameManager.Instance.UpgradeAttribute("BulletDamage"));
        specialDamageButton.onClick.AddListener(() => GameManager.Instance.UpgradeAttribute("SpecialDamage"));

        // Skill buttons
        specialAttackBtn.onClick.AddListener(() => GameManager.Instance.SpecialAttackStart());
    }
}
