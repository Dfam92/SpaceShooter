using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D bulletRb;
    private PlayerControl playerControl;

    private void Start()
    {
     
      playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
   

    }
    private void Update()
    {
        bulletOutBounds();
    }
    void FixedUpdate()
    {
        bulletMovement();
    }

    private void bulletMovement()
    {
        bulletRb.AddForce(Vector2.up, ForceMode2D.Impulse);
        transform.rotation = Quaternion.identity;
    }
    private void bulletOutBounds()
    {
            if (transform.position.y > ScreenBounds.yEnemyBound + 1f)
            {
                this.gameObject.SetActive(false);
                if (playerControl.bulletCount > 0 && playerControl.playerIsDestroyed == false)
                {
                    playerControl.bulletCount -= 1;
                }
            }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("BossSting") || collision.gameObject.CompareTag("PowerUpCase"))
        {
            if (playerControl.bulletCount > 0 && playerControl.playerIsDestroyed == false)
            {
                playerControl.bulletCount -= 1;
            }
            this.gameObject.SetActive(false);
        }
       
    }
}
