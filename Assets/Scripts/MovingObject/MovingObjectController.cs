using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    [Header("Components")]
    public Transform movingObject;
    public Transform patrolPoints;

    [Header("Variables")]
    public float speed = 10f;
    public float minDistanceForChange = 0.1f;

    private int currentIndex = 0;
    private bool isStopped = false;

    private void Start()
    {
        if (movingObject == null || patrolPoints == null)
        {
            Debug.LogError("Some components are null! This should not be the case!");
        }
    }

    private void Update()
    {
        if (isStopped)
        {
            return;
        }

        // approximate equality between vectors
        if (Vector2.Distance(movingObject.position, patrolPoints.GetChild(currentIndex).position) < minDistanceForChange)
        {
            currentIndex = (currentIndex + 1) % patrolPoints.childCount;
        }

        movingObject.position = Vector2.MoveTowards(movingObject.position, patrolPoints.GetChild(currentIndex).position, speed);
    }

    public void SetIsStopped(bool isStopped)
    {
        this.isStopped = isStopped;
    }
}
