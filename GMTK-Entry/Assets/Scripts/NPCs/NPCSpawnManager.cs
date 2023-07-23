using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private NPCPlayableCharacter npcPrefab;
    [SerializeField] private float timeBetweenSpawns;

    public void InitializeNewNPC(Transform protagonistTransform)
    {
        StartCoroutine(SpawnNPCCoroutine(protagonistTransform));
    }

    IEnumerator SpawnNPCCoroutine(Transform protagonistTransform)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);

        //Search for the closest spawnpoint to the protagonistTransform and spawn the NPC right there

        SpawnPoint closestSpawnPoint = spawnPoints[0];
        float closestDistance = Vector3.Distance(protagonistTransform.position, closestSpawnPoint.transform.position);
        for(int i = 1; i < spawnPoints.Length; i++)
        {
            float distance = Vector3.Distance(protagonistTransform.position, spawnPoints[i].transform.position);
            if(distance < closestDistance)
            {
                closestSpawnPoint = spawnPoints[i];
                closestDistance = distance;
            }
        }

        NPCPlayableCharacter newNPC = Instantiate(npcPrefab, closestSpawnPoint.exitPoint.position, closestSpawnPoint.exitPoint.rotation);
        NPCPlayableCharacter.selectedPlayer = newNPC;
    }
}
