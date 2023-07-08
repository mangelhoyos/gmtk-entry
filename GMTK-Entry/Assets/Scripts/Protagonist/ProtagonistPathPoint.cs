using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProtagonistPathPoint : MonoBehaviour
{
    [SerializeField] private UnityEvent onPointReached;
    private bool pointReached = false;
    public float delayTime;

    public void ReachPoint()
    {
        if (!pointReached)
        {
            onPointReached?.Invoke();
            pointReached = true;
        }
    }
}
