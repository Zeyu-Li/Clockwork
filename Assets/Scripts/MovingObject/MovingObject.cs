using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public MovingObjectController controller;

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
}
