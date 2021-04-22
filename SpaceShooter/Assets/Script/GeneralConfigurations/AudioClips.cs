using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip explosionEnemy;
    public AudioClip explosionPlayer;
    
    public static bool enemyIsDestroyed;
    public static bool playerIsDestroyed;
    //public static bool isMoving;
    

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
            audioPlayer.PlayOneShot(explosionPlayer,0.5f);
            playerIsDestroyed = false;
        }
       
       /* else if( isMoving == true)
        {
            audioPlayer.PlayOneShot(shipEngine);
            isMoving = false;
        }*/
        
    }

}
