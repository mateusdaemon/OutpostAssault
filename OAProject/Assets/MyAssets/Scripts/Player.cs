using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerShooter playerShooter;
    [SerializeField] private PlayerOrientation playerOrientation;


    // Update is called once per frame
    void Update()
    {
        playerMove.Move(playerInput.MoveDirection);

        if (playerInput.Attack != 0)
        {
            playerShooter.Shoot(playerOrientation.LookDirection);
        }
    }
}
