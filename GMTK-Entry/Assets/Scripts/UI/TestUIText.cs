using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIText : MonoBehaviour
{
    [SerializeField] private UIHandler uiHandler;

    [ContextMenu("Test points")]
    public void TestPoints()
    {
        uiHandler.AddPointsToMarker(400, transform.position);
    }

    [ContextMenu("Reduce health")]
    public void ReduceHealth()
    {
        uiHandler.ReduceHealth();
    }
}
