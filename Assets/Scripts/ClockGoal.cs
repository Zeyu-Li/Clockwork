using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockGoal : MonoBehaviour
{
    public SaveData saveData;
    public AudioSource audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("We win");
        audioClip.Play();
        gameObject.SetActive(false);
        saveData.IncreaseLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
