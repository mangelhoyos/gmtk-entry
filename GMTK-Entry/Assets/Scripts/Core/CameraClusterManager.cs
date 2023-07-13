using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClusterManager : MonoBehaviour
{
    [SerializeField] private Vector3[] cameraPositions;
    [SerializeField] private Camera mainCamera;

    public void SetCameraPosition(int index)
    {
        mainCamera.transform.position = cameraPositions[index];
    }

}
