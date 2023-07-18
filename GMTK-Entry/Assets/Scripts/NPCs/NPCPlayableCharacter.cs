using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NPCPlayableCharacter : MonoBehaviour
{
    public static NPCPlayableCharacter selectedPlayer;

    private bool isDead = false;

    [HideInInspector] public NavMeshAgent Agent;

    public Action OnDeath;

    public bool isActive = false;

    public bool justOnce = false;

    //Movement
    private Camera CameraUsed;
    [Header("Movement")]
    [SerializeField]
    [Range(0, 0.99f)]
    private float Smoothing = 0.25f;
    [SerializeField]
    private float TargetLerpSpeed = 1;

    private Vector3 TargetDirection;
    private float LerpTime = 0;
    private Vector3 LastDirection;
    private Vector3 MovementVector;

    //Attack
    [Header("Attack")]
    [SerializeField] private PlayerAnimationManager playerAnimationManager;

    [SerializeField] private GameObject attackRangePoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask layerMask;

    private void OnEnable()
    {
        CameraUsed = Camera.main;
        Agent = GetComponent<NavMeshAgent>();
        if(selectedPlayer == null)
        {
            selectedPlayer = this;
        }   
    }

    void Update()
    {
        if (!isDead)
        {
            if (selectedPlayer == this)
            {
                GeneratePlayerBehaviour();
            }
            else
            {
                GenerateEnemyBehaviour();
            }
        }
    }

    private void GenerateEnemyBehaviour()
    {

    }

    private void GeneratePlayerBehaviour()
    {
        if (isActive && !justOnce)
        {
            MovementVector = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                MovementVector += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                MovementVector += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                MovementVector += Vector3.right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                MovementVector += Vector3.back;
            }

            MovementVector.Normalize();
            if (MovementVector != LastDirection)
            {
                LerpTime = 0;
            }
            LastDirection = MovementVector;

            MovementVector = CameraUsed.transform.TransformDirection(MovementVector);
            MovementVector.x = MovementVector.y;
            MovementVector.y = 0;

            TargetDirection = Vector3.Lerp(
                TargetDirection,
                MovementVector,
                Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - Smoothing))
            );

            Agent.Move(TargetDirection * Agent.speed * Time.deltaTime);
            Vector3 lookDirection = MovementVector.normalized;
            if (lookDirection != Vector3.zero)
            {
                Agent.transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.LookRotation(lookDirection),
                    Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - Smoothing))
                );
            }

            LerpTime += Time.deltaTime;

            //Diaz

            Debug.Log("Magnitute: " + MovementVector.magnitude);

            playerAnimationManager.ChangePlayerMovementAnimation(MovementVector.magnitude);

            if (Input.GetKey(KeyCode.Mouse0) && !justOnce)
            {
                playerAnimationManager.ChangePlayerAttackAnimation();

                Agent.enabled = false;

                if (Physics.OverlapSphere(attackRangePoint.transform.position, attackRadius, layerMask).Length > 0)
                {
                    Physics.OverlapSphere(attackRangePoint.transform.position, attackRadius, layerMask)[0].GetComponent<ProtagonistAgent>().PlayerReceivedDamage();
                }

                justOnce = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackRangePoint.transform.position, attackRadius);
    }

    public void KillEnemy()
    {
        selectedPlayer = null;
        isDead = true;
        isActive = false;
        playerAnimationManager.ChangeProtaToDeadAnimation();
    }

    public static void SelectNPC(NPCPlayableCharacter selectedCharacter)
    {
        selectedPlayer = selectedCharacter;
    }
}
