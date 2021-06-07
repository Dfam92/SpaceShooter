using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBubble : MonoBehaviour
{
    private PlayerControl player;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletOutBounds();
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
        if(collision.gameObject.CompareTag("Player"))
        {
            player = GameObject.Find("Player").GetComponent<PlayerControl>();
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

    }
}
