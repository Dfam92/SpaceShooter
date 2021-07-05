using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public AudioClip firsHit;
    public Rigidbody2D enemyRb;

    private GameManager gameManager;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float speed;
    [SerializeField] private int enemyHealth;
    
    public int enemyPoint;
    private int enemyCount = 1;
    
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.isActive == true)
        {
            EnemyMovement();
        }
        
    }
    public void EnemyMovement()
    {
        enemyRb.AddRelativeForce(Vector2.down*speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.CompareTag("PlayerBullet"))
        {
            TakeHit();
            
            if(enemyHealth > 0)
            {
                audioSource.PlayOneShot(firsHit, 0.5f);
                spriteRenderer.color = Color.red;
                speed += 3;
            }
            
            if(enemyHealth < 1)
            {
                MultiplyPoints();
                AudioClips.enemyIsDestroyed = true;
                Destroy(this.gameObject);
                gameManager.UpdateScore(enemyPoint);
                gameManager.UpdateEnemies(enemyCount);
            }
        }
        else if (collision.CompareTag("AlienShield"))
        {
            {
                Destroy(this.gameObject);
                Shield.ShieldHit();
            }
            
        }
        
    }
    private void TakeHit()
    {
        enemyHealth -= 1;
    }
    private void MultiplyPoints()
    {
        if(PlayerControl.isMultiplying2x == true)
        {
            enemyPoint *= 2;
        }
        else if(PlayerControl.isMultiplying4x == true)
        {
            enemyPoint *= 4;
        }
    }
}
