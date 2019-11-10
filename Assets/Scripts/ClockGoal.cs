using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("We win");
        gameObject.SetActive(false);
    }
}
