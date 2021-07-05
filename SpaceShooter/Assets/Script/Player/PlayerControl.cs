using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameManager gameManager;
    private AudioSource playerAudioSource;
    private Animator animPlayer;

    public Rigidbody2D playerRb;
    public GameObject AlienShield;
    public GameObject bulletPlayer;
    public AudioClip bulletSound;
   

    public static bool isMultiplying2x;
    public static bool isMultiplying4x;
    public static bool sideBullets;

    private int timeToStopPowerUp;

    //For mobile active the Joystick
    public Joystick joystick;
    public Joystick fireButton;

    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        animPlayer = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void Update()
    // if mobile desactive this.
    {
        if (GameManager.isActive == true)
        {
            PlayerShoot();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.isActive == true)
        {
            PlayerMovement();
            PlayerOutBounds();
        }
    }
    //When Mobile, change the Inputs to Joystick.horizontal and Joystick.vertical, dont forget to instantiate the class Joystick.
    private void PlayerMovement()
    {
        //For Play in PC active this

         float horizontalInput = Input.GetAxis("Horizontal");
         float verticalInput = Input.GetAxis("Vertical");
         playerRb.AddForce(Vector2.right * speed * horizontalInput);
         playerRb.AddForce(Vector2.up * speed * verticalInput);

         if (verticalInput > 0 || horizontalInput > 0 || horizontalInput <0)
         {
             animPlayer.SetFloat("Move", 1f);
            
         }
         else
         {
             animPlayer.SetFloat("Move", -1f);
            

         }

        // For play Mobile
        /*float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector2.right * speed * joystick.Horizontal);
        playerRb.AddForce(Vector2.up * speed * joystick.Vertical);

        if (joystick.Vertical > 0 || joystick.Horizontal > 0 || joystick.Horizontal < 0)
        {
            animPlayer.SetFloat("Move", 1f);
            //AudioClips.isMoving = true;
        }
        else
        {
            animPlayer.SetFloat("Move", -1f);
            //AudioClips.isMoving = false;

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
    {
        // For play in Pc active this
        
        Vector3 bulletPos = new Vector3(transform.position.x-0.05f, transform.position.y + 0.5f, transform.rotation.z);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Enemy") && !AlienShield.activeInHierarchy)
        {
            AudioClips.playerIsDestroyed = true;
            Destroy(gameObject);
            gameManager.GameOver();

        }
        else if (collision.CompareTag("Boss") && !AlienShield.activeInHierarchy)
        {
            AudioClips.playerIsDestroyed = true;
            Destroy(gameObject);
            gameManager.GameOver();

        }
        else if(collision.CompareTag("Shield"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            AudioClips.shieldIsActivated = true;
        }
        else if (collision.CompareTag("BulletCase"))
        {
            sideBullets = true;
            timeToStopPowerUp = 15;
            StartCoroutine(StopPowerUp());
        }
        else if(collision.CompareTag("Multiply2x"))
        {
            isMultiplying2x = true;
            timeToStopPowerUp = 15;
            StartCoroutine(StopPowerUp());
        }
        else if (collision.CompareTag("Multiply4x"))
        {
            isMultiplying4x = true;
            timeToStopPowerUp = 10;
            StartCoroutine(StopPowerUp());
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
}
