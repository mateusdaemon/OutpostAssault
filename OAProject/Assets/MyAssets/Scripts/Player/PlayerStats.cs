using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject, ISerializationCallbackReceiver
{
    public int level = 1;
    public int maxLevel = 10;
    public float currentXP = 0;
    public float xpToNextLevel = 100;

    [Header("Player Attributes")]
    public float attackSpeed = 1f;
    public float maxAttackSpeed = 4f;
    public float bulletDamage = 3f;
    public float maxBulletDamage = 12f;
    public float specialDuration = 5f;
    public float maxSpecialDuration = 11f;
    public float specialColdown = 30f;

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

        GameManager.Instance.PlayerLevelUp();
        HudManager.Instance.UpdatePlayerLevel(level);
    }

    public float AttackSpeed()
    {
        return attackSpeed;
    }

    public float AttackDamage()
    {
        return bulletDamage;
    }

    public float SpecialDuration()
    {
        return specialDuration;
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        ResetPlayerStats();
    }

    public void ResetPlayerStats()
    {
        level = 1;
        maxLevel = 10;
        currentXP = 0;
        xpToNextLevel = 100;
        attackSpeed = 1f;
        bulletDamage = 3f;
        specialDuration = 5f;
    }
}
