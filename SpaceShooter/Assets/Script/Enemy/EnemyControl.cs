using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Rigidbody2D enemyRb;
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyMovement();
    }
    public void EnemyMovement()
    {
        enemyRb.AddRelativeForce(Vector2.down*speed);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        
        if(collision.CompareTag("PlayerBullet"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
