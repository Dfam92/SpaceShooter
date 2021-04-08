using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D enemyBulletRb;
    private PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        BulletDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BulletDirection()
    {
        enemyBulletRb.AddForce(Vector2.down, ForceMode2D.Impulse);
        transform.rotation = Quaternion.identity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(player.gameObject);
            Destroy(this.gameObject);
        }
    }
}
