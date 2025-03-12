using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private PlayerAnimation playerAnim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 direction)
    {
        Vector3 movement = new Vector3(direction.x, 0, direction.y) * speed;
        rb.AddForce(movement);
        
        // Normaliza o valor entre 0 e 1 e dispara o evento
        float movementMagnitude = movement.magnitude / speed;
        PlayerEvents.TriggerMove(movementMagnitude);
    }
}
