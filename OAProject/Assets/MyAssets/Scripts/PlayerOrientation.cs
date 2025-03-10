using System;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    public Vector3 LookDirection { get; private set; }
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationSpeed = 10f;

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
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
