using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;





public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToStopInvulnerabilty;
    [SerializeField] private float timeToStopFreeze;
    [SerializeField] private int bulletCapacity;

    private GameManager gameManager;
    private AudioSource playerAudioSource;
    private Animator animPlayer;
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    public Rigidbody2D playerRb;
    public GameObject alienShield;
    public GameObject bulletPlayer;
    public AudioClip bulletSound;
    public GameObject explosion;
    public GameObject diagonalBullet;
    public GameObject diagonalBullet2;
    public GameObject rewardAdsButton;
    public GameObject pauseButton;

    public  bool isMultiplying2x;
    public  bool isMultiplying4x;
    private int timeToStopPowerUp4x;
    public  bool onSideBullets;
    private int timeToStopPowerUpS;
    public  bool onDiagonalBullets;
    private int timeToStopPowerUpD;
    public bool playerIsDestroyed;
    private bool extraLifeChance = true;
    
    public int bulletCount;
    public int maxBulletCapacity;
    private float paralyzeTime = 3;
    
    public Vector2 playerStartPos;
    
    //For mobile active the Joystick
    public Joystick joystick;
    public Joystick fireButton;
    private int timeToStopPowerUp2x;

    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        animPlayer = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.transform.position = playerStartPos;
       
    }
    private void Awake()
    {
        playerStartPos = new Vector3(0,-4,0);
    }

    private void Update()
    {
        if (GameManager.isActive && !PauseMenu.isPaused)
        {
            //if mobile desactive this.
            //PlayerShoot();
            // Dont Disable This.
            RespawnPlayer();
            FixDoubleCollisionBullet();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.isActive && !playerIsDestroyed)
        {
            PlayerMovement();
            PlayerOutBounds();
        }
    }
    //When Mobile, change the Inputs to Joystick.horizontal and Joystick.vertical, dont forget to instantiate the class Joystick.
    private void PlayerMovement()
    {
        
        //For Play in PC active this
        if(!gameManager.isFreezed)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            playerRb.AddForce(Vector2.right * speed * horizontalInput);
            playerRb.AddForce(Vector2.up * speed * verticalInput);

            if (verticalInput > 0 )
            {
                if (playerRb.mass < 100)
                {
                    animPlayer.SetFloat("Move", 0.5f);
                    animPlayer.SetBool("TurnRight", false);
                    animPlayer.SetBool("TurnLeft", false);
                }

            }
            else if (horizontalInput < 0)
            {
                animPlayer.SetBool("TurnLeft", true);
                animPlayer.SetBool("TurnRight", false);
                animPlayer.SetFloat("Move", -0.9f);
            }
            else if(horizontalInput > 0)
            {
                animPlayer.SetBool("TurnRight", true);
                animPlayer.SetBool("TurnLeft", false);
                animPlayer.SetFloat("Move", -0.9f);
            }
            else
            {
                animPlayer.SetFloat("Move", -0.5f);
                animPlayer.SetBool("TurnRight", false);
                animPlayer.SetBool("TurnLeft", false);
            }
        }


        // For play Mobile

        /*if(!gameManager.isFreezed)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            playerRb.AddForce(Vector2.right * speed * joystick.Horizontal);
            playerRb.AddForce(Vector2.up * speed * joystick.Vertical);

         if (joystick.Vertical > 0 )
            {
                if (playerRb.mass < 100)
                {
                    
                    animPlayer.SetFloat("Move", 0.5f);
                    animPlayer.SetBool("TurnRight", false);
                    animPlayer.SetBool("TurnLeft", false);
                }

            }
            else if (joystick.Horizontal < 0)
            {
                animPlayer.SetBool("TurnLeft", true);
                animPlayer.SetBool("TurnRight", false);
                animPlayer.SetFloat("Move", -0.9f);
            }
            else if(joystick.Horizontal > 0)
            {
                animPlayer.SetBool("TurnRight", true);
                animPlayer.SetBool("TurnLeft", false);
                animPlayer.SetFloat("Move", -0.9f);
            }
            else
            {
                animPlayer.SetFloat("Move", -0.5f);
                animPlayer.SetBool("TurnRight", false);
                animPlayer.SetBool("TurnLeft", false);
            }
        }*/
        


    }

    private void PlayerOutBounds()
    {
        Vector2 topPos = new Vector2(transform.position.x, -ScreenBounds.yPlayerBound);
        Vector2 rightPos = new Vector2(ScreenBounds.xPlayerBound, transform.position.y);
        Vector2 leftPos = new Vector2(-ScreenBounds.xPlayerBound, transform.position.y);
        Vector2 botPos = new Vector2(transform.position.x, ScreenBounds.yPlayerBound);
        
        if(transform.position.x > ScreenBounds.xPlayerBound)
        {
            transform.position = leftPos;
        }
        else if(transform.position.x < -ScreenBounds.xPlayerBound)
        {
            transform.position = rightPos;
        }
        else if(transform.position.y > ScreenBounds.yPlayerBound)
        {
            transform.position = botPos;
        }
        else if(transform.position.y < -ScreenBounds.yPlayerBound)
        {
            transform.position = topPos;
        }
        
    }
    public void PlayerShoot()
    {if (!playerIsDestroyed && bulletCount > -1 && bulletCount < bulletCapacity)
        {
            //For play in Pc active this
            
            /*Vector3 bulletPos = new Vector3(transform.position.x + 0.02f, transform.position.y  + 0.5f, transform.rotation.z);
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                ObjectPooler.Instance.SpawnFromPool("PlayerBullet", bulletPos, transform.rotation);
                playerAudioSource.PlayOneShot(bulletSound, 1.0f);
                bulletCount += 1;

                if (onSideBullets == true)
                {
                    Vector3 bulletPos2 = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.rotation.z);
                    Vector3 bulletPos3 = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.rotation.z);

                    ObjectPooler.Instance.SpawnFromPool("SideBullet", bulletPos2, transform.rotation);
                    ObjectPooler.Instance.SpawnFromPool("SideBullet", bulletPos3, transform.rotation);
                }

                if (onDiagonalBullets == true)
                {
                    Vector2 bulletPos2 = new Vector2(transform.position.x, transform.position.y );
                    Vector2 bulletPos3 = new Vector2(transform.position.x, transform.position.y );

                    ObjectPooler.Instance.SpawnFromPool("DiagonalBullets", bulletPos2, diagonalBullet2.transform.rotation);
                    ObjectPooler.Instance.SpawnFromPool("DiagonalBullets2", bulletPos3, diagonalBullet.transform.rotation);
                }
            }*/

            //for play Mobile active this
            Vector3 bulletPos = new Vector3(transform.position.x + 0.02f, transform.position.y + 0.5f, transform.rotation.z);

            {
                ObjectPooler.Instance.SpawnFromPool("PlayerBullet", bulletPos, transform.rotation);
                playerAudioSource.PlayOneShot(bulletSound, 1.0f);
                bulletCount += 1;
            }

            if (onSideBullets == true)
            {
                Vector3 bulletPos2 = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.rotation.z);
                Vector3 bulletPos3 = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.rotation.z);

                ObjectPooler.Instance.SpawnFromPool("SideBullet", bulletPos2, transform.rotation);
                ObjectPooler.Instance.SpawnFromPool("SideBullet", bulletPos3, transform.rotation);
            }

            if(onDiagonalBullets == true)
            {
                Vector2 bulletPos2 = new Vector2(transform.position.x , transform.position.y);
                Vector2 bulletPos3 = new Vector2(transform.position.x , transform.position.y);
               
                ObjectPooler.Instance.SpawnFromPool("DiagonalBullets", bulletPos2, diagonalBullet2.transform.rotation);
                ObjectPooler.Instance.SpawnFromPool("DiagonalBullets2", bulletPos3, diagonalBullet.transform.rotation);
            }

        }
    }

    private void RespawnPlayer()
    {
        
        if (gameManager.isReborned)
        {
            onDiagonalBullets = false;
            onSideBullets = false;
            isMultiplying2x = false;
            isMultiplying4x = false;
            spriteRenderer.enabled = true;
            animPlayer.SetBool("IsReborned", true);
            StartCoroutine("StopInvulnerability");
            //must be lower than Stop Invulnerability
            StartCoroutine(StopFreeze());
            playerIsDestroyed = false;
           

        }
        else
        {
            if(!playerIsDestroyed)
            {
                circleCollider.enabled = true;
            }
           
        }
        
    }
    private void FixDoubleCollisionBullet()
    {
        if(bulletCount < 0)
        {
            bulletCount = 0;
        }
    }
    private void DestroyPlayer()
    {
        
        circleCollider.enabled = false;
        spriteRenderer.enabled = false;
        playerIsDestroyed = true;
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        AudioClips.playerIsDestroyed = true;
        gameManager.UpdateLife(-1);
        animPlayer.Rebind();

        if (gameManager.lifeScore < 0 && extraLifeChance == true)
        {
            pauseButton.SetActive(false);
            Time.timeScale = 0;
            rewardAdsButton.SetActive(true);
            extraLifeChance = false;
        }
        else if (extraLifeChance == false && gameManager.lifeScore < 0)
        {
             gameManager.GameOver();
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !alienShield.activeInHierarchy || collision.CompareTag("Boss") && !alienShield.activeInHierarchy
            || collision.CompareTag("EnemyBullet") && !alienShield.activeInHierarchy || collision.CompareTag("Explosion") && !alienShield.activeInHierarchy)
        {
            DestroyPlayer();
            bulletCount = 0;
        }
        else if(collision.CompareTag("Shield"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            AudioClips.shieldIsActivated = true;
        }
        else if (collision.CompareTag("BossSting") && !alienShield.activeInHierarchy)
        {
            playerRb.mass = 100;
            BossAudioClips.playerIsEletrified = true;
            transform.GetChild(2).gameObject.SetActive(true);
            spriteRenderer.color = Color.gray;
            StartCoroutine(StopParalysis());
        }
        else if (collision.CompareTag("SideBullets"))
        {
            onSideBullets = true;
            timeToStopPowerUpS += 15;
            AudioClips.extraBulletsOn = true;
            StartCoroutine(StopPowerUpSBullets());
        }
        else if (collision.CompareTag("DiagonalBullets"))
        {
            onDiagonalBullets = true;
            timeToStopPowerUpD += 15;
            AudioClips.extraBulletsOn = true;
            StartCoroutine(StopPowerUpDBullets());
        }
        else if(collision.CompareTag("Multiply2x"))
        {
            isMultiplying4x = false;
            isMultiplying2x = true;
            timeToStopPowerUp2x += 10;
            AudioClips.is2xOn = true;
            StartCoroutine(StopPowerUp2x());
        }
        else if (collision.CompareTag("Multiply4x"))
        {
            isMultiplying2x = false;
            isMultiplying4x = true;
            timeToStopPowerUp4x += 10;
            AudioClips.is4xOn = true;
            StartCoroutine(StopPowerUp4x());
                
        }
        else if (collision.CompareTag("ExtraLife"))
        {
            gameManager.UpdateLife(1);
            AudioClips.extraLife = true;
        }
        else if (collision.CompareTag("BulletCase"))
        {
            if (bulletCapacity < maxBulletCapacity)
            {
                bulletCapacity += 1;
                AudioClips.extraBulletsOn = true;
            }
        }
        else if (collision.CompareTag("SpeedUp"))
        {
            speed += 1f;
            AudioClips.extraBulletsOn = true;

        }

    }
  
    IEnumerator StopPowerUpSBullets()
    {
        yield return new WaitForSeconds(timeToStopPowerUpS);
        if (onSideBullets == true)
        {
            onSideBullets = false;
            timeToStopPowerUpS = 0;
        }
    }
    IEnumerator StopPowerUpDBullets()
    {

        yield return new WaitForSeconds(timeToStopPowerUpD);
        if (onDiagonalBullets == true)
        {
            onDiagonalBullets = false;
            timeToStopPowerUpD = 0;
        }
    }
    IEnumerator StopPowerUp2x()
    {
        yield return new WaitForSeconds(timeToStopPowerUp2x);
        if (isMultiplying2x == true)
        {
            isMultiplying2x = false;
            timeToStopPowerUp2x = 0;
        }
    }
    IEnumerator StopPowerUp4x()
    {
        yield return new WaitForSeconds(timeToStopPowerUp4x);
        if (isMultiplying4x == true)
        {
            isMultiplying4x = false;
            timeToStopPowerUp4x = 0;
        }
    }
    IEnumerator StopParalysis()
    {
        yield return new WaitForSeconds(paralyzeTime);
        playerRb.mass = 1;
        spriteRenderer.color = Color.white;

    }
    IEnumerator StopInvulnerability()
    {
        if(!playerIsDestroyed)
        {
            yield break;
        }
        yield return new WaitForSeconds(timeToStopInvulnerabilty);
        gameManager.isReborned = false;
        animPlayer.SetBool("IsReborned", false);

    }
    IEnumerator StopFreeze()
    {
        yield return new WaitForSeconds(timeToStopFreeze);
        gameManager.isFreezed = false;
    }
   
}
