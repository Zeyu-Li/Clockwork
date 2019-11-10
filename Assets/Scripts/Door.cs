using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isDoorLocked = true;
    public bool isTimeLimited = false;
    public float limitDuration = 3.0f;

    private void Start()
    {
        UpdateDoorLock();
    }

    public void ActivateDoor()
    {
        StopAllCoroutines();
        isDoorLocked = !isDoorLocked;
        UpdateDoorLock();
        StartCoroutine(TimeLimit());
    }

    private void UpdateDoorLock()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isDoorLocked);
        }
    }

    IEnumerator TimeLimit()
    {
        yield return new WaitForSeconds(limitDuration);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        Debug.Log("Finish");
    }
}
