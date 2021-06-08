using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public Rigidbody2D bossRb;
    public GameObject bossSting;
    public GameObject bossBubble;
    
    private bool isTurning;
    private bool isMoving = true;

    private int healthBoss = 10 ;
    public bool bossIsDefeated;

    [SerializeField] private float speed;
    [SerializeField] private float timeToTurn;
    [SerializeField] private float turnAngle;
    [SerializeField] private float timeToShotStings;
    [SerializeField] private float timeToShotBubbles;

    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("FireSting", 2, timeToShotStings);
        InvokeRepeating("FireBubble", 2, timeToShotBubbles);
    }

    private void Update()
    {
        BossDestroyed();
        Debug.Log(healthBoss);
        Debug.Log(bossIsDefeated);
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

    private void FireSting()
    {
       Instantiate(bossSting,transform.position,transform.rotation);
    }

    private void FireBubble()
    {
        Instantiate(bossBubble,transform.position,transform.rotation);
    }

    public void BossDestroyed()
    {
        if (healthBoss < 1)
        {
            bossIsDefeated = true;
            gameManager.bossDefeated = true;
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

            if (healthBoss ==  0)
            {
                Destroy(this.gameObject,1);

            }
            
            else if (healthBoss < 15)
            {
                spriteRenderer.color = Color.red;
                speed += 4;
            }
           
        }
    }

}
