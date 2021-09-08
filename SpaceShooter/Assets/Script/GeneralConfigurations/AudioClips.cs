using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource audioPlayer;
    public AudioClip explosionEnemy;
    public AudioClip explosionPlayer;
    public AudioClip shieldsEnabled;
    public AudioClip shieldHitted;
    public AudioClip bulletsEnabled;
    public AudioClip powerUp2x;
    public AudioClip powerUp4x;
    public AudioClip hearthPowerUp;
    
    
    public static bool enemyIsDestroyed;
    public static bool playerIsDestroyed;
    public static bool shieldIsActivated;
    public static bool shieldWasHitted;
    public static bool extraBulletsOn;
    public static bool is2xOn;
    public static bool is4xOn;
    public static bool extraLife;
    

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
       
    }
    // Update is called once per frame
    void Update()
    {
        if ( enemyIsDestroyed == true)
        {
            audioPlayer.PlayOneShot(explosionEnemy, 0.25f);
            enemyIsDestroyed = false;
        }
        else if ( playerIsDestroyed == true)
        {
            audioPlayer.PlayOneShot(explosionPlayer,0.35f);
            playerIsDestroyed = false;
        }
        else if ( shieldIsActivated == true)
        {
            if(!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(shieldsEnabled, 0.3f);
                shieldIsActivated = false;
            }
        }
        else if (shieldWasHitted == true)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(shieldHitted, 1f);
                shieldWasHitted = false;
            }
        }
        else if (extraBulletsOn == true)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(bulletsEnabled, 0.2f);
                extraBulletsOn = false;
            }
        }
        else if (is2xOn == true)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(powerUp2x, 0.2f);
                is2xOn = false;
            }
        }
        else if (is4xOn == true)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(powerUp4x, 0.2f);
                is4xOn = false;
            }
        }
        else if (extraLife == true)
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(hearthPowerUp, 0.5f);
                extraLife = false;
            }
        }

    }

}
