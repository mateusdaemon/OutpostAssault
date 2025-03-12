using System;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<float> OnMove;
    public static event Action OnShoot;

    public static void TriggerMove(float speed)
    {
        OnMove?.Invoke(speed);
    }

    public static void TriggerShoot()
    {
        OnShoot?.Invoke();
    }
}
