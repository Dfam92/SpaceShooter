using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -ScreenBounds.yPlayerBound-1f)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("AlienShield"))
        {
            
            Destroy(this.gameObject);
        }
        
    }

}
