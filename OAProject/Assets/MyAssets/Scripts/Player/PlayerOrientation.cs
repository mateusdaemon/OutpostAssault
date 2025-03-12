using System;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    public Vector3 LookDirection { get; private set; }
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float raycastMaxDistance = 100f;

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        if (mainCamera == null) return;

        // Garantir que o mouse está dentro dos limites da tela
        Vector3 clampedMousePos = Input.mousePosition;
        clampedMousePos.x = Mathf.Clamp(clampedMousePos.x, 0, Screen.width);
        clampedMousePos.y = Mathf.Clamp(clampedMousePos.y, 0, Screen.height);

        Ray ray = mainCamera.ScreenPointToRay(clampedMousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastMaxDistance, groundLayer))
        {
            Vector3 lookAtPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            LookDirection = (lookAtPosition - transform.position).normalized;

            if (LookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(LookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
