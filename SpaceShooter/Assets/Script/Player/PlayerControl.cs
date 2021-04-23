using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;

    public Rigidbody2D playerRb;
    public GameObject bulletPlayer;
    public AudioClip bulletSound;
    private AudioSource playerAudioSource;
    private Animator animPlayer;
    private GameManager gameManager;
    private EnemyControl enemyControl;

    //For mobile active the Joystick
    public Joystick joystick;
    public Joystick fireButton;
    public GameObject AlienShield;


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
        /*if (GameManager.isActive == true)
        {
            PlayerShoot();
        }*/
       
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

        /* float horizontalInput = Input.GetAxis("Horizontal");
         float verticalInput = Input.GetAxis("Vertical");
         playerRb.AddForce(Vector2.right * speed * horizontalInput);
         playerRb.AddForce(Vector2.up * speed * verticalInput);

         if (verticalInput > 0 || horizontalInput > 0 || horizontalInput <0)
         {
             animPlayer.SetFloat("Move", 1f);
             //AudioClips.isMoving = true;
         }
         else
         {
             animPlayer.SetFloat("Move", -1f);
             //AudioClips.isMoving = false;

         }*/

        // For play Mobile
        float horizontalInput = Input.GetAxis("Horizontal");
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

        }
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
        
        /*Vector2 bulletPos = new Vector2(transform.position.x, transform.position.y + 0.5f);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPlayer, bulletPos, transform.rotation);
            playerAudioSource.PlayOneShot(bulletSound, 1.0f);
        }*/
        

        //for play Mobile active this
        Vector2 bulletPos = new Vector2(transform.position.x, transform.position.y + 0.5f);
        {
            Instantiate(bulletPlayer, bulletPos, transform.rotation);
            playerAudioSource.PlayOneShot(bulletSound, 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Enemy") && !AlienShield.activeInHierarchy)
        {
            AudioClips.playerIsDestroyed = true;
            Destroy(gameObject);
            gameManager.GameOver();

        }
       else if(collision.CompareTag("Shield"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            
        }
    }
}
