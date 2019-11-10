using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public MovingObjectController controller;
    public Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // todo: add spirit here
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            controller.SetIsStopped(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // todo: add spirit here
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            controller.SetIsStopped(false);
        }
    }

    private void Update()
    {
        if (Input.GetButton("ToggleTime"))
        {
            controller.SetIsStopped(!controller.isStopped);
        }
    }
}
