using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProtagonistAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ProtagonistPathHandler pathHandler;

    private bool isFighting = false;

    [Header("NPC Detection settings")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask unitLayerMask;
    [SerializeField] private float retreatDistance;

    //Diaz
    [SerializeField] private AimToTarget aimToTargetManager;
    [SerializeField] private ProtaAnimationManager protaAnimationManager;
    [SerializeField] private PistolManager pistolManager;

    private bool isDead = false;
    private void Start()
    {
        StartCoroutine(CheckCurrentState());
    }

    public void ChangeAgentTarget(Vector3 destination, bool combatMode = false)
    {
        agent.SetDestination(destination);
        isFighting = combatMode;
    }

    IEnumerator CheckCurrentState()
    {
        while(!isDead)
        {
            //Debug.Log("Magnitute: " + agent.velocity.magnitude);

            protaAnimationManager.ChangeProtaMovementAnimation(isFighting, agent.velocity.magnitude);

            NPCPlayableCharacter NPCFound = FindNearestEnemy();

            if(NPCFound != null)
            {
                Debug.Log("NPC found");
                
                pathHandler.StopPath();
                isFighting = true;

                Debug.Log("Fighting");
                ChangeAgentTarget(RetreatAndShoot(NPCFound), true);
                agent.updateRotation = false;
                Vector3 npcFixedPosition = NPCFound.transform.position;
                npcFixedPosition.y = transform.position.y;
                transform.LookAt(npcFixedPosition);

                yield return null;
                continue;
            }
            else if(NPCFound == null && isFighting)
            {
                Debug.Log("Exiting fighting state...");
                ChangeAgentTarget(transform.position, false);
                pistolManager.StopShooting();
                aimToTargetManager.DisableAimToTarget();
                yield return new WaitForSeconds(1f);
                isFighting = false;
                agent.updateRotation = true;
            }

            Debug.Log("Following path..");
            pathHandler.StartPath();
            ChangeAgentTarget(pathHandler.GetActualPathTargetPosition());

            yield return new WaitForSeconds(.1f);
        }

        agent.updateRotation = false;
        agent.updatePosition = false;
    }

    public void SetAsDead()
    {
        isDead = true;
        protaAnimationManager.ChangeProtaToDeadAnimation();
    }    

    private Vector3 RetreatAndShoot(NPCPlayableCharacter NPCFound)
    {
        // Calcula la dirección desde el jugador hacia el enemigo
        Vector3 retreatDestination;
        Vector3 NPCFixedPosition = NPCFound.transform.position;
        NPCFixedPosition.y = transform.position.y;
        Vector3 direction = transform.position - NPCFixedPosition;

        // Calcula la posición de destino hacia donde retroceder
        retreatDestination = transform.position + direction.normalized * 5f;

        // Verifica si la posición de destino está dentro del área válida del NavMeshAgent
        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatDestination, out hit, 5f, NavMesh.AllAreas))
        {
            // Si la posición de destino es válida, mueve al jugador hacia ella
            agent.SetDestination(hit.position);
        }

        if(Vector3.Distance(transform.position, NPCFixedPosition) >= retreatDistance)
        {
            retreatDestination = transform.position;
        }

        //Diaz
        aimToTargetManager.ChangeTargetPosition(NPCFound.transform.position);

        pistolManager.StartShooting();

        return retreatDestination;
    }

    private NPCPlayableCharacter FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, unitLayerMask);

        NPCPlayableCharacter nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            Debug.Log("NPC FOUND!");
            NPCPlayableCharacter enemyCharacter = collider.GetComponent<NPCPlayableCharacter>();
            if (enemyCharacter != null && enemyCharacter.isActive)
            {
                // Comprobar si hay una pared o algo enfrente del enemigo
                Vector3 directionToEnemy = enemyCharacter.transform.position - transform.position;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToEnemy, out hit, detectionRadius))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("NPC"))
                    {
                        // Calcular la distancia al enemigo y actualizar el más cercano
                        float distanceToEnemy = Vector3.Distance(transform.position, enemyCharacter.transform.position);
                        if (distanceToEnemy < nearestDistance)
                        {
                            nearestEnemy = enemyCharacter;
                            nearestDistance = distanceToEnemy;
                        }
                    }
                }
                else
                {
                    Debug.Log("Wall found");
                }
                
            }
        }

        return nearestEnemy;
    }

    public void PlayerReceivedDamage()
    {
        protaAnimationManager.ChangeProtaTakingDamageAnimation();
        UIHandler.Instance.ReduceHealth();
    }

}
