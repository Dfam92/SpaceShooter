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
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        bulletOutBounds();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gameManager.gameOver)
        {
            StingDirection();
        }
       
    }
    private void bulletOutBounds()
    {
        if (transform.position.y < -ScreenBounds.yEnemyBound - 1f || transform.position.y > ScreenBounds.yEnemyBound + 1f
            || transform.position.x < -ScreenBounds.xEnemyBound - 1f || transform.position.x > ScreenBounds.xEnemyBound + 1f)
        {
            this.gameObject.SetActive(false);
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
            this.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("AlienShield"))
        {
            Shield.ShieldHit();
            AudioClips.shieldWasHitted = true;
            this.gameObject.SetActive(false);
        }
        else if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            this.gameObject.SetActive(false);
            BossAudioClips.stingDestroyed = true;
        }
    }
    
}
