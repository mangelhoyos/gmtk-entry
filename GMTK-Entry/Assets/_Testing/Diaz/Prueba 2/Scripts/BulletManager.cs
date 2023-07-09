using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<NPCPlayableCharacter>())
        {
            collision.gameObject.GetComponent<NPCPlayableCharacter>().KillEnemy();
        }
        Destroy(gameObject);
    }

}
