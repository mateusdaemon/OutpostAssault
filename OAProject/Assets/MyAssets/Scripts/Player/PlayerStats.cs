using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int level = 1;
    public int maxLevel = 10;
    public float currentXP = 0;
    public float xpToNextLevel = 100;

    [Header("Player Attributes")]
    public float attackSpeed = 1f;
    public float bulletDamage = 10f;
    public float specialDuration = 5f;

    public void AddXP(float amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel && level < maxLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentXP = 0;
        level++;
        xpToNextLevel += xpToNextLevel * 0.3f;

        GameManager.Instance.GrantUpgradePoint();
    }
}
