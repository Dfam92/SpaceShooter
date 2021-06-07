using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSting : MonoBehaviour
{
    public Rigidbody2D stingRb;
    private PlayerControl player;
    private GameManager gameManager;

    [SerializeField]private float speed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (transform.position.y < -ScreenBounds.yEnemyBound - 1f || transform.position.y > ScreenBounds.yEnemyBound + 1f || transform.position.x < -ScreenBounds.xEnemyBound - 1f || transform.position.x > ScreenBounds.xEnemyBound + 1f)
        {
            Destroy(this.gameObject);
        }

    }

    private void StingDirection()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();

        if (player != null)
        {
            stingRb.AddForce((player.transform.position-transform.position)*speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(player.gameObject);
            Destroy(this.gameObject);
            gameManager.GameOver();
            AudioClips.playerIsDestroyed = true;
        }
        else if (collision.gameObject.CompareTag("AlienShield"))
        {
            Shield.ShieldHit();
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
