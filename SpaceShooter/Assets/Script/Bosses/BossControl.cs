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

    private int healthBoss = 20 ;
    
    [SerializeField] private float speed;
    [SerializeField] private float timeToTurn;
    [SerializeField] private float turnAngle;
    [SerializeField] private float timeToShotStings;
    [SerializeField] private float timeToShotBubbles;

    private SpriteRenderer spriteRenderer;

    private GameManager gameManager;

    private AudioSource bossAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossAudioSource = GetComponent<AudioSource>();
        InvokeRepeating("FireSting", 2, timeToShotStings);
        InvokeRepeating("FireBubble", 2, timeToShotBubbles);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(FadeAudioSource.StartFade(bossAudioSource, 10, 0.25f));
    }
   
    private void Update()
    {
       if(GameManager.gameOver == true)
        {
            bossAudioSource.Stop();
            CancelInvoke();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        BossMovimentation();
    }
    private void LateUpdate()
    {
        StartCoroutine(BossLimitBounds());
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
        BossAudioClips.bubbleFired = true;
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
    private void OnDestroy()
    {
        gameManager.bossDefeated = true;
    }
    private void TakeHit()
    {
        healthBoss -= 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeHit();

            if (healthBoss == 0)
            {
                gameManager.UpdateScore(10000);
                Destroy(this.gameObject, 1);

            }

            else if (healthBoss < 10)
            {
                spriteRenderer.color = Color.red;
                speed += 4;
                timeToShotBubbles = 1;
            }

        }
        else if (collision.gameObject.CompareTag("AlienShield"))
        {
            Shield.ShieldHit();
            AudioClips.shieldWasHitted = true;
        }

        }

    IEnumerator BossLimitBounds()
    {
        yield return new WaitForSeconds(5);
        OutOfBounds();
    }
}
