using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public const float rotationSpeed = 50f;

    public void CollectCollectable()
    {
        UIHandler.Instance.AddPointsToMarker(500, transform.position);
        AudioManager.instance.Play("Points");
        Destroy(gameObject);
    }

    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
