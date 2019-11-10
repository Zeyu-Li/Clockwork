using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public List<Door> correspondingDoors;
    public Sprite buttonOn;
    public Sprite buttonOff;
    public AudioSource audioClip;

    private SpriteRenderer spriteRenderer;
    private bool buttonPressed;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (buttonPressed) return;
        buttonPressed = true;

        foreach (Door door in correspondingDoors)
        {
            door.ActivateDoor();
            audioClip.Play();
        }

        spriteRenderer.sprite = buttonOn;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!buttonPressed) return;
        buttonPressed = false;
        spriteRenderer.sprite = buttonOff;
    }
}
