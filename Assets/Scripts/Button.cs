using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public List<Door> correspondingDoors;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Door door in correspondingDoors)
        {
            door.ActivateDoor();
        }
    }
}
