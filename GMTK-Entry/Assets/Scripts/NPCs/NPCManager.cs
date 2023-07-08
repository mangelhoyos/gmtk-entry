using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    List<NPCPlayableCharacter> npcCharacters = new List<NPCPlayableCharacter>();
    [SerializeField] NPCPlayableCharacter npcPrefab;
    
    public void GenerateNPC(Transform spawnPosition)
    {
        NPCPlayableCharacter newCharacter = Instantiate(npcPrefab, spawnPosition.position, Quaternion.identity);
        npcCharacters.Add(newCharacter);
        newCharacter.OnDeath += () => NPCDead(newCharacter);  
    }

    public void NPCDead(NPCPlayableCharacter npc)
    {
        npcCharacters.Remove(npc);
        SelectNPC();
    }

    public void SelectNPC()
    {
        int randomNPCIndex = Random.Range(0, npcCharacters.Count);
        NPCPlayableCharacter.selectedPlayer = npcCharacters[randomNPCIndex];
    }
}
