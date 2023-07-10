using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void Update()
    {
        transform.LookAt(target.transform);
    }
}
