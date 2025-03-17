using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    public float Attack { get; private set; }
    public Vector2 MoveDirection { get; private set; }
    public Vector2 MousePosition { get; private set; }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    private void FixedUpdate()
    {
        Attack = inputActions.Player.Attack.ReadValue<float>();
        MoveDirection = inputActions.Player.Move.ReadValue<Vector2>();
        MousePosition = Input.mousePosition;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
}
