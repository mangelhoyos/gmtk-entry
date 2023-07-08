using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    public void ChangePlayerMovementAnimation(float velocity)
    {
        if (isDead) return;

        if (velocity > 0.5f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    public void ChangePlayerAttackAnimation()
    {
        if (isDead) return;

        animator.SetTrigger("IsAttacking");
    }

    public void ChangeProtaToDeadAnimation()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
    }
}
