using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public Rigidbody2D bossRb;

    private bool isTurning;
    private bool isMoving = true;

    [SerializeField] private int healthBoss;
    [SerializeField] private float speed;
    [SerializeField] private float timeToTurn;
    [SerializeField] private float turnAngle;

    private SpriteRenderer spriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BossMovimentation();
    }
    private void LateUpdate()
    {
        OutOfBounds();
    }

    private void BossMovimentation()
    {
        if(isMoving == true)

        {
            bossRb.AddRelativeForce(Vector2.up*speed);
        }

        if(transform.position.x > ScreenBounds.xEnemyBound / 4  )

        {
            StartCoroutine(TurnRight());
            isTurning = true;
        }
        else if (transform.position.x > -ScreenBounds.xEnemyBound / 2)

        {
            StartCoroutine(TurnLeft());
            isTurning = true;
        }
    }
    IEnumerator TurnRight()

    {
        if(isTurning == true)
        {
            yield return new WaitForSeconds(timeToTurn);
            bossRb.AddTorque(-turnAngle);
            isTurning = false;
        }
        
    }

    IEnumerator TurnLeft()

    {
        if(isTurning == true)
        {
            yield return new WaitForSeconds(timeToTurn);
            bossRb.AddTorque(turnAngle);
            isTurning = false;
        }
        
    }

    void OutOfBounds()
    {
        Vector2 topPos = new Vector2(transform.position.x, ScreenBounds.yEnemyBound+0.5f);
        Vector2 rightPos = new Vector2(ScreenBounds.xEnemyBound+0.5f, transform.position.y);
        Vector2 leftPos = new Vector2(-ScreenBounds.xEnemyBound-0.5f, transform.position.y);
        Vector2 botPos = new Vector2(transform.position.x, -ScreenBounds.yEnemyBound-0.5f);

        if (transform.position.y < -ScreenBounds.yEnemyBound - 0.5f)
        {
            gameObject.transform.position = topPos;
        }
        if (transform.position.x < -ScreenBounds.xEnemyBound - 0.5f)
        {
            gameObject.transform.position = rightPos;
        }
        else if (transform.position.x > ScreenBounds.xEnemyBound + 0.5f)
        {
            gameObject.transform.position = leftPos;
        }
        //telepot top from bot
        else if (transform.position.y > ScreenBounds.yEnemyBound + 0.5f)
        {
            gameObject.transform.position = botPos;
        }
    }

    private void TakeHit()
    {
        healthBoss -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            TakeHit();
            
           if(healthBoss < 15)
            {
                spriteRenderer.color = Color.red;
                speed += 4;
            }
            if(healthBoss < 1)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
