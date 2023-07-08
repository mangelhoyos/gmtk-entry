using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistPathHandler : MonoBehaviour
{
    [SerializeField] private ProtagonistPathPoint[] pathPoints;
    [SerializeField] private ProtagonistAgent protagonistHandler;

    private bool isSearchingForPath = true;
    Coroutine actualCoroutine = null;
    int actualFollowPoint = 0;

    private void Start()
    {
        StartPath();
    }

    public void StartPath()
    {
        if(actualCoroutine == null)
        {
            Debug.Log("Start Path");
            actualCoroutine = StartCoroutine(FollowPath());
            isSearchingForPath = true;
        }
    }

    public void StopPath()
    {
        if(actualCoroutine != null)
        {
            Debug.Log("Stop Path");
            StopCoroutine(actualCoroutine);
            isSearchingForPath = false;
            actualCoroutine = null;
        }
    }

    public Vector3 GetActualPathTargetPosition() => pathPoints[actualFollowPoint].transform.position;
    
    private IEnumerator FollowPath()
    {
        while (isSearchingForPath && actualFollowPoint < pathPoints.Length)
        {
            Vector3 fixedDistancePosition = pathPoints[actualFollowPoint].transform.position;
            fixedDistancePosition.y = protagonistHandler.transform.position.y;

            if (Vector3.SqrMagnitude(protagonistHandler.transform.position - fixedDistancePosition) < 0.25f)
            {
                Debug.Log("New path point reached " + actualFollowPoint);
                yield return new WaitForSeconds(pathPoints[actualFollowPoint].delayTime);
                pathPoints[actualFollowPoint].ReachPoint();
                actualFollowPoint++;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
    
}
