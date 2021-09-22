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
        if(collision.CompareTag("PlayerBullet") )
        {
            TakeHit();
            
            if(enemyHealth > 0)
            {
                audioSource.PlayOneShot(firsHit, 0.5f);
                spriteRenderer.color = Color.red;
                speed += 2;
            }
            
            if(enemyHealth < 1)
            {
                DestroyEnemy();
            }
        }
        else if(collision.CompareTag("Explosion"))
        {
            DestroyEnemy();
        }
        else if (collision.CompareTag("AlienShield"))
        {
            {
                Destroy(this.gameObject);
                Shield.ShieldHit();
                AudioClips.shieldWasHitted = true;
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
    private void UpdateGameManagerCalls()
    {
        if(gameManager.levelSelect == 1)
        {
            var easyPoints = enemyPoint / 4;
            gameManager.UpdateScore(easyPoints);
        }
        else if (gameManager.levelSelect == 2)
        {
            var normalPoints = enemyPoint / 2;
            gameManager.UpdateScore(normalPoints);
        }
        else
        {
            gameManager.UpdateScore(enemyPoint);
        }

        
        gameManager.UpdateEnemies(enemyCount);
    }
    private void DestroyEnemy()
    {
        MultiplyPoints();
        AudioClips.enemyIsDestroyed = true;
        //Instantiate(explosion, transform.position, transform.rotation);
        ObjectPooler.Instance.SpawnFromPool("EnemyExploded", transform.position, transform.rotation);
        Destroy(this.gameObject);
        UpdateGameManagerCalls();
    }
}
