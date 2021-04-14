using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D enemyBulletRb;
    private PlayerControl player;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        BulletDirection();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            AudioClips.playerIsDestroyed = true;
            Destroy(player.gameObject);
            Destroy(this.gameObject);
            gameManager.GameOver();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BotBound") || collision.CompareTag("LeftBound") || collision.CompareTag("RightBound"))
        {
            Destroy(this.gameObject);
        }
    }
}
