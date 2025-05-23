using System;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<float, float> OnMove;
    public static event Action OnShoot;
    public static event Action OnSpecial;
    public static event Action OnTakeDamage;
    public static event Action OnLevelUp;

    public static void TriggerMove(float moveX, float moveY)
    {
        OnMove?.Invoke(moveX, moveY);
    }

    public static void TriggerShoot()
    {
        OnShoot?.Invoke();
    }

    public static void TriggerSpecial()
    {
        OnSpecial?.Invoke();
    }

    public static void TriggerPlayerDmg()
    {
        OnTakeDamage?.Invoke();
    }

    public static void TriggerLevelUp()
    {
        OnLevelUp?.Invoke();
    }
}
