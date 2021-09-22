using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalBullets : MonoBehaviour
{
    
    public GameObject player;
    private Rigidbody2D diagonalRb;
    [SerializeField]private float speedBullet;

    private void Start()
    {
        diagonalRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        BulletOutBounds();
        
    }
    private void FixedUpdate()
    {
        BulletMovement();

    }
    public void BulletMovement()
    {
        
        diagonalRb.AddForce(Vector2.up, ForceMode2D.Impulse);
        diagonalRb.AddRelativeForce(Vector2.right,ForceMode2D.Impulse);
   
    }
    public void BulletOutBounds()
    {
        if (transform.position.y > ScreenBounds.yEnemyBound + 1f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("BossSting") || collision.gameObject.CompareTag("PowerUpCase"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
