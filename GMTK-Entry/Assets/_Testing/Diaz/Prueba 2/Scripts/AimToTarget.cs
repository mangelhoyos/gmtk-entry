using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimToTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            // Calcula la nueva posici�n suavemente utilizando Lerp
            Vector3 newPosition = Vector3.Lerp(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Actualiza la posici�n del objetivo
            target.transform.position = newPosition;

            // Verifica si se alcanz� la posici�n objetivo con una tolerancia peque�a
            if (Vector3.Distance(target.transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    public void ChangeTargetPosition(Vector3 movePosition)
    {
        if (!isMoving)
        {
            targetPosition = movePosition;
            isMoving = true;
        }
    }
}
