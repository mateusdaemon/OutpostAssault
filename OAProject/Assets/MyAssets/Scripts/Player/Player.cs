using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerShooter playerShooter;
    [SerializeField] private PlayerOrientation playerOrientation;

    void FixedUpdate()
    {
        playerMove.Move(playerInput.MoveDirection);
        playerShooter.Shoot(playerOrientation.LookDirection, playerInput.Attack);
    }

    public void UpdatePlayerAtt(PlayerStats playerStats)
    {
        playerShooter.SetShootInterval(playerStats.attackSpeed);
    }
}
 