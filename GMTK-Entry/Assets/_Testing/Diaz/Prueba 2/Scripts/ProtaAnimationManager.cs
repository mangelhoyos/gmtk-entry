using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtaAnimationManager : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    public void ChangeProtaMovementAnimation(bool isInCombat, float velocity)
    {
        if (isDead) return;

        if (velocity > 0.5f)
        {
            if (!isInCombat)
            {
                // Mover hacia atr�s
                animator.SetBool("IsMovingForward", false);
                animator.SetBool("IsMovingBackward", true);
            }
            else
            {
                // Mover hacia adelante
                animator.SetBool("IsMovingForward", true);
                animator.SetBool("IsMovingBackward", false);
            }
        }
        else
        {
            // No hay movimiento, desactiva todas las animaciones de movimiento
            animator.SetBool("IsMovingForward", false);
            animator.SetBool("IsMovingBackward", false);
        }
    }

    public void ChangeProtaToDeadAnimation()
    {
        isDead = true;
        animator.SetBool("IsDead", true);

        // Si el personaje est� muerto, no activar la animaci�n de recibir da�o
        animator.SetBool("IsTakingDamage", false);
    }

    public void ChangeProtaToTakingDamageAnimation()
    {
        if (isDead) return;

        // Activar la animaci�n de recibir da�o
        animator.SetBool("IsTakingDamage", true);

    }

    public void ChangeProtaToNormalAnimation()
    {
        if (isDead) return;

        // Activar la animaci�n de recibir da�o

        animator.SetTrigger("IsTakingDamage");
    }
}
