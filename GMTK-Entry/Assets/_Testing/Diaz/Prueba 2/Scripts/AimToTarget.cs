using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimToTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float fadeSpeed = 1f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    [SerializeField] private MultiAimConstraint bodyMultiAimConstraint;
    [SerializeField] private MultiAimConstraint handMultiAimConstraint;


    private void Update()
    {
        if (isMoving)
        {
            // Calcula la nueva posición suavemente utilizando Lerp
            Vector3 newPosition = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Actualiza la posición del objetivo
            target.transform.position = newPosition;

            // Verifica si se alcanzó la posición objetivo con una tolerancia pequeña
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
            EnableAimToTarget();
        }
    }

    public void EnableAimToTarget()
    {
        if (bodyMultiAimConstraint != null && handMultiAimConstraint != null)
        {
            StartCoroutine(MoveBodyAndHandWeight());
        }
    }

    private IEnumerator MoveBodyAndHandWeight()
    {
        if(bodyMultiAimConstraint.weight != 1f && handMultiAimConstraint.weight != 1f)
        {
            bodyMultiAimConstraint.weight += 0.1f;
            handMultiAimConstraint.weight += 0.1f;
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(MoveBodyAndHandWeight());
        }
        else
        {
            StopCoroutine(MoveBodyAndHandWeight());
        }
    }

    public void DisableAimToTarget()
    {
        if (bodyMultiAimConstraint != null && handMultiAimConstraint != null)
        {
            bodyMultiAimConstraint.weight = 0f;
            handMultiAimConstraint.weight = 0f;
            StopCoroutine(MoveBodyAndHandWeight());
        }
    }
}
