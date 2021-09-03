using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToStopInvulnerabilty;
    [SerializeField] private float timeToStopFreeze;

    private GameManager gameManager;
    private AudioSource playerAudioSource;
    private Animator animPlayer;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    public Rigidbody2D playerRb;
    public GameObject alienShield;
    public GameObject bulletPlayer;
    public AudioClip bulletSound;
    public GameObject explosion;
   
    public static bool isMultiplying2x;
    public static bool isMultiplying4x;
    public static bool sideBullets;
    public bool playerIsDestroyed;

    private int timeToStopPowerUp;
    private float paralyzeTime = 3;
    public Vector2 playerStartPos;
    
    //For mobile active the Joystick
    public Joystick joystick;
    public Joystick fireButton;
   
    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        animPlayer = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStartPos = new Vector2(0, -4);
        this.transform.position = playerStartPos;
       
    }

    private void Update()
     
    {
        if (GameManager.isActive && !PauseMenu.isPaused)
        {
            //if mobile desactive this.
            PlayerShoot();
            // Dont Disable This.
            RespawnPlayer();
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
                }

            }
            else if (horizontalInput < 0)
            {
                animPlayer.SetBool("TurnLeft", true);
                animPlayer.SetBool("TurnRight", false);
            }
            else if(horizontalInput > 0)
            {
                animPlayer.SetBool("TurnRight", true);
                animPlayer.SetBool("TurnLeft", false);
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
                }

            }
            else if (joystick.Horizontal < 0)
            {
                animPlayer.SetBool("TurnLeft", true);
                animPlayer.SetBool("TurnRight", false);
            }
            else if(joystick.Horizontal > 0)
            {
                animPlayer.SetBool("TurnRight", true);
                animPlayer.SetBool("TurnLeft", false);
            }
            else
            {
                animPlayer.SetFloat("Move", -0.5f);
                animPlayer.SetBool("TurnRight", false);
                animPlayer.SetBool("TurnLeft", false);
            }
        }
        */


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
    {if(!playerIsDestroyed)
        {
            //For play in Pc active this

            Vector3 bulletPos = new Vector3(transform.position.x - 0.05f, transform.position.y + 0.5f, transform.rotation.z);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(bulletPlayer, bulletPos, transform.rotation);
                playerAudioSource.PlayOneShot(bulletSound, 1.0f);

                if (sideBullets == true)
                {

                    Vector3 bulletPos2 = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.rotation.z);
                    Vector3 bulletPos3 = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.rotation.z);

                    Instantiate(bulletPlayer, bulletPos2, transform.rotation);
                    Instantiate(bulletPlayer, bulletPos3, transform.rotation);
                }
            }



            //for play Mobile active this
            /*Vector3 bulletPos = new Vector3(transform.position.x -0.05f, transform.position.y + 0.5f,transform.rotation.z);
            {
                Instantiate(bulletPlayer, bulletPos, transform.rotation);
                playerAudioSource.PlayOneShot(bulletSound, 1.0f);
            }

            if (sideBullets == true)
            {

                Vector3 bulletPos2 = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.rotation.z);
                Vector3 bulletPos3 = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.rotation.z);

                Instantiate(bulletPlayer, bulletPos2, transform.rotation);
                Instantiate(bulletPlayer, bulletPos3, transform.rotation);
            }*/
        }

    }

    private void RespawnPlayer()
    {
        
        if (gameManager.isReborned)
        {
            
            sideBullets = false;
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
                
                boxCollider.enabled = true;
               
            }
           
        }
        
    }
    private void DestroyPlayer()
    {
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        playerIsDestroyed = true;
        transform.GetChild(1).gameObject.SetActive(true);
        AudioClips.playerIsDestroyed = true;
        gameManager.UpdateLife(-1);
        animPlayer.Rebind();

        if (gameManager.lifeScore < 0)
        {
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !alienShield.activeInHierarchy || collision.CompareTag("Boss") && !alienShield.activeInHierarchy
            || collision.CompareTag("EnemyBullet") && !alienShield.activeInHierarchy)
        {
            DestroyPlayer();
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
            StartCoroutine(StopParalysis());
        }
        else if (collision.CompareTag("BulletCase"))
        {
            sideBullets = true;
            timeToStopPowerUp = 15;
            AudioClips.extraBulletsOn = true;
            StartCoroutine(StopPowerUp());
        }
        else if(collision.CompareTag("Multiply2x"))
        {
            isMultiplying2x = true;
            timeToStopPowerUp = 15;
            AudioClips.is2xOn = true;
            StartCoroutine(StopPowerUp());
        }
        else if (collision.CompareTag("Multiply4x"))
        {
            isMultiplying4x = true;
            timeToStopPowerUp = 10;
            AudioClips.is4xOn = true;
            StartCoroutine(StopPowerUp());
        }
        else if (collision.CompareTag("ExtraLife"))
        {
            gameManager.UpdateLife(1);
        }
        
    }
  
    
    IEnumerator StopPowerUp()
    {
        
        yield return new WaitForSeconds(timeToStopPowerUp);
        if( sideBullets == true)
        {
            sideBullets = false;
        }
        else if(isMultiplying2x == true)
        {
            isMultiplying2x = false;
        }
        else if (isMultiplying4x == true)
        {
            isMultiplying4x = false;
        }

    }
    IEnumerator StopParalysis()
    {
        yield return new WaitForSeconds(paralyzeTime);
        playerRb.mass = 1;

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
