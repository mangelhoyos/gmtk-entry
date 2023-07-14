using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtaAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator pistolAnimator;
    private bool isDead = false;

    public void ChangeProtaMovementAnimation(bool isInCombat, float velocity)
    {
        if (isDead) return;

        if (velocity > 1f)
        {
            if (!isInCombat)
            {
                // Mover hacia adelante
                animator.SetBool("IsMovingFoward", true);
                animator.SetBool("IsMovingBackward", false);
            }
            else
            {
                // Mover hacia atras
                animator.SetBool("IsMovingFoward", false);
                animator.SetBool("IsMovingBackward", true);
            }
        }
        else
        {
            // No hay movimiento, desactiva todas las animaciones de movimiento
            animator.SetBool("IsMovingFoward", false);
            animator.SetBool("IsMovingBackward", false);
        }
    }

    public void ChangeProtaToDeadAnimation()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
    }

    public void ChangeProtaTakingDamageAnimation()
    {
        if (isDead) return;

        // Activar la animación de recibir daño
        animator.SetTrigger("IsTakingDamage");
    }

    public void ShootPistolAnimation()
    {
        if (isDead) return;
        // Activar la animacion de disparo
        animator.SetTrigger("IsShooting");
    }
}
