using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockGoal : MonoBehaviour
{
    public SaveData saveData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("We win");
        gameObject.SetActive(false);
        saveData.IncreaseLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
