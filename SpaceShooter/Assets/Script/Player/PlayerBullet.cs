using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    protected Rigidbody2D bulletRb;
    private PlayerControl playerControl;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
    private void Update()
    {
        BulletOutBounds();
    }
    void FixedUpdate()
    {
        BulletMovement();
    }

    public virtual void BulletMovement()
    {
        bulletRb.AddForce(Vector2.up, ForceMode2D.Impulse);
    }
    public virtual void BulletOutBounds()
    {
            if (transform.position.y > ScreenBounds.yEnemyBound + 0.5f)
            {
                this.gameObject.SetActive(false);
                if (playerControl.bulletCount > -1 && !playerControl.playerIsDestroyed)
                {
                    playerControl.bulletCount -= 1;
                }
            }
            

    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") ||
            collision.gameObject.CompareTag("BossSting") || collision.gameObject.CompareTag("PowerUpCase") || collision.gameObject.CompareTag("Explosion"))
        {
            if (playerControl.bulletCount > -1 && playerControl.playerIsDestroyed == false)
            {
                playerControl.bulletCount -= 1;
            }
            this.gameObject.SetActive(false);
        }
       
    }
}
