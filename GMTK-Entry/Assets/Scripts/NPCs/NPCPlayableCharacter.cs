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

    private NavMeshAgent Agent;

    public Action OnDeath;

    //Movement
    private Camera CameraUsed;
    [SerializeField]
    [Range(0, 0.99f)]
    private float Smoothing = 0.25f;
    [SerializeField]
    private float TargetLerpSpeed = 1;

    private Vector3 TargetDirection;
    private float LerpTime = 0;
    private Vector3 LastDirection;
    private Vector3 MovementVector;

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
 
    }

    public void KillEnemy()
    {

    }

    public static void SelectNPC(NPCPlayableCharacter selectedCharacter)
    {
        selectedPlayer = selectedCharacter;
    }
}
