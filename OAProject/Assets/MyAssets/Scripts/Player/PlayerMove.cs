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
        // Dire��o de movimento global
        Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;

        // Aplica o movimento no mundo
        rb.AddForce(moveDirection * speed);

        // Agora vamos calcular a dire��o local para anima��o
        Vector3 localMove = transform.InverseTransformDirection(moveDirection); // Movimento no espa�o local

        // A convers�o j� foi feita, agora podemos calcular a dire��o da anima��o
        Vector2 animationDirection = new Vector2(localMove.x, localMove.z); // Usando x para direita e z para frente/tr�s

        // Enviar evento de anima��o para ser tratado
        PlayerEvents.TriggerMove(animationDirection.x, animationDirection.y);
    }
}
