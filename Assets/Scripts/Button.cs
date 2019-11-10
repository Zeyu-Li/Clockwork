using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public List<Door> correspondingDoors;
    public Sprite buttonOn;
    public Sprite buttonOff;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Door door in correspondingDoors)
        {
            door.ActivateDoor();
        }

        spriteRenderer.sprite = buttonOn;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sprite = buttonOff;
    }
}
