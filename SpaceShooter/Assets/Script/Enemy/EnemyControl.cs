using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Rigidbody2D enemyRb;
    [SerializeField] private float speed;
    private float yPos = 4;
    

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
        Vector2 leftPos =  new Vector2(-Bounds.xBound, yPos );
        Vector2 topPos = new Vector2(-Bounds.xBound, Bounds.yBound);
        if(collision.CompareTag("PlayerBullet"))
        {
            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("RightBound"))
        {
            gameObject.transform.position = leftPos;
            yPos += -1;
        }
        else if (collision.CompareTag("BotBound"))
        {
            gameObject.transform.position = topPos;
            yPos = Bounds.yBound;
        }
    }
}
