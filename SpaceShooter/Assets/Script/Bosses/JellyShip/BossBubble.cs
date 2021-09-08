using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBubble : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletOutBounds();
    }
    private void bulletOutBounds()
    {
        if (transform.position.y < -ScreenBounds.yEnemyBound - 1f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("AlienShield"))
        {
            Shield.ShieldHit();
            Destroy(this.gameObject);
            AudioClips.shieldWasHitted = true;
        }

    }
}
