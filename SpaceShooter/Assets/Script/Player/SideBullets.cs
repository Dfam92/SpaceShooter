using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBullets : PlayerBullet
{
    public override void BulletOutBounds()
    {
        if (transform.position.y > ScreenBounds.yEnemyBound + 1f)
        {
            this.gameObject.SetActive(false);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("BossSting") || collision.gameObject.CompareTag("PowerUpCase"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
