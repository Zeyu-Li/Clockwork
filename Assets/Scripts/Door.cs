using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isDoorLocked = true;

    private void Start()
    {
        UpdateDoorLock();
    }

    public void ActivateDoor()
    {
        isDoorLocked = !isDoorLocked;
        UpdateDoorLock();
    }

    private void UpdateDoorLock()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isDoorLocked);
        }
    }
}
