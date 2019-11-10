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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
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
}
