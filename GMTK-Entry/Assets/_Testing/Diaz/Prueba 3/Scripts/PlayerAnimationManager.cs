using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangePlayerMovementAnimation(float velocity)
    {
        if (isDead) return;

        if (velocity > 1f)
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
