using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        //PowerUpOutOfBounds
        if (transform.position.y < -ScreenBounds.yPlayerBound-2f)
        {
            this.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("AlienShield"))
        {
            this.gameObject.SetActive(false);
        }
        
    }

}
