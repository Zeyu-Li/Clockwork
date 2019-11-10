using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicManager : MonoBehaviour
{

    public bool soul;
    public Player player;
    public AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(audioSource);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        print(player.isSoulTime);
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
}
