using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D bulletRb;
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
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("BossSting"))
        {
            Destroy(this.gameObject);
        }
       
    }
}
