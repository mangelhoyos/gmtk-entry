using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<NPCPlayableCharacter>())
        {
            collision.gameObject.GetComponent<NPCPlayableCharacter>().KillEnemy();
        }
        Destroy(gameObject);
    }

}
