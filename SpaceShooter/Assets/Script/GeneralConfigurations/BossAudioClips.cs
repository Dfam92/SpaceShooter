using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudioClips : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip electricShock;
    public AudioClip stingDestroy;
    public AudioClip bubbleFire;

    public static bool enemyIsShocked;
    public static bool stingDestroyed;
    public static bool bubbleFired;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyIsShocked == true)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(electricShock, 0.3f);
                enemyIsShocked = false;
            }
        }
        else if (stingDestroyed == true)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(stingDestroy, 0.2f);
                stingDestroyed = false;
            }
        }
        else if (bubbleFired == true)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(bubbleFire, 0.2f);
                bubbleFired = false;
            }
        }
    }
}
