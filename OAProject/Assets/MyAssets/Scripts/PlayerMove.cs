using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 direction)
    {
        // rb.linearVelocity = new Vector3(direction.x, rb.linearVelocity.y, direction.y) * speed;
        rb.AddForce(new Vector3(direction.x, 0, direction.y) * speed);
    }
}
