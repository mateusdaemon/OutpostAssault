using UnityEngine;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private PlayerOrientation playerOrientation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 input)
    {
        // Direção de movimento global
        Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;

        // Aplica o movimento no mundo
        rb.AddForce(moveDirection * speed);

        // Agora vamos calcular a direção local para animação
        Vector3 localMove = transform.InverseTransformDirection(moveDirection); // Movimento no espaço local

        // A conversão já foi feita, agora podemos calcular a direção da animação
        Vector2 animationDirection = new Vector2(localMove.x, localMove.z); // Usando x para direita e z para frente/trás

        // Enviar evento de animação para ser tratado
        PlayerEvents.TriggerMove(animationDirection.x, animationDirection.y);
    }
}
