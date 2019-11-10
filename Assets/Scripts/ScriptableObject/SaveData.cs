using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SaveData")]
public class SaveData : ScriptableObject
{
    public int currentLevel = 1;

    public void IncreaseLevel(int currentLevel)
    {
        if (this.currentLevel == currentLevel)
        {
            this.currentLevel++;
        }

        if (currentLevel > 12)
        {
            Debug.Log("You finish the game!");
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
    }
}
