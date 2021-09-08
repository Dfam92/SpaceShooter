using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSting : MonoBehaviour
{
    public Rigidbody2D stingRb;
    private PlayerControl player;
    
    [SerializeField]private float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
    private void Update()
    {
        bulletOutBounds();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.gameOver)
        {
            StingDirection();
        }
       
    }
    private void bulletOutBounds()
    {
        if (transform.position.y < -ScreenBounds.yEnemyBound - 1f || transform.position.y > ScreenBounds.yEnemyBound + 1f
            || transform.position.x < -ScreenBounds.xEnemyBound - 1f || transform.position.x > ScreenBounds.xEnemyBound + 1f)
        {
            Destroy(this.gameObject);
        }

    }

    private void StingDirection()
    {
        if (player != null)
        {
            stingRb.AddForce((player.transform.position-transform.position)*speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            Destroy(this.gameObject);
            
        }
        else if (collision.gameObject.CompareTag("AlienShield"))
        {
            Shield.ShieldHit();
            AudioClips.shieldWasHitted = true;
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(this.gameObject);
            BossAudioClips.stingDestroyed = true;
        }
    }
    
}
