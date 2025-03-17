using System;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<float, float> OnMove;
    public static event Action OnShoot;

    public static void TriggerMove(float moveX, float moveY)
    {
        OnMove?.Invoke(moveX, moveY);
    }

    public static void TriggerShoot()
    {
        OnShoot?.Invoke();
    }
}
