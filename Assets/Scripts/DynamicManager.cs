using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicManager : MonoBehaviour
{
    public static DynamicManager instance1;
    public static DynamicManager instance2;
    public bool soul;
    public Player player;
    public AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance1 is null)
        {
            instance1 = this;
            DontDestroyOnLoad(audioSource);
        }
        else if (instance2 is null)
        {
            instance2 = this;
            DontDestroyOnLoad(audioSource);
        }
        else if (instance1 != this && instance2 != this)
        {
            Destroy(audioSource);
        }

        if (SceneManager.GetActiveScene().name == "Title")
        {
            instance1 = null;
            instance2 = null;
            Destroy(audioSource);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(audioSource is null))
        {
            if (player is null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
            if (player.isSoulTime && soul)
            {
                audioSource.mute = false;
            }
            else if (!player.isSoulTime && !soul)
            {
                audioSource.mute = false;
            }
            else
            {
                audioSource.mute = true;
            }
        }

        if (SceneManager.GetActiveScene().name == "Title")
        {
            instance1 = null;
            instance2 = null;
            Destroy(audioSource);
            Destroy(this);
        }
    }
}
