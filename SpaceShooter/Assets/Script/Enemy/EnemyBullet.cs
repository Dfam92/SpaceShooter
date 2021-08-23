using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D enemyBulletRb;

    // Start is called before the first frame update
    void Start()
    {
        
        BulletDirection();
       
    }

    private void Update()
    {
        bulletOutBounds();
    }

    private void BulletDirection()
    {
        enemyBulletRb.AddForce(Vector2.down, ForceMode2D.Impulse);
        transform.rotation = Quaternion.identity;
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
