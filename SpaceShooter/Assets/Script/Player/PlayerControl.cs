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

    

    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        animPlayer = GetComponent<Animator>();
    }


    private void Update()
    {
        PlayerShoot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        PlayerMovement();
        PlayerOutBounds();
    }

    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
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

        }
    }

    private void PlayerOutBounds()
    {
        Vector2 topPos = new Vector2(transform.position.x, Bounds.yPlayerBound);
        Vector2 rightPos = new Vector2(Bounds.xPlayerBound, transform.position.y);
        Vector2 leftPos = new Vector2(-Bounds.xPlayerBound, transform.position.y);
        Vector2 botPos = new Vector2(transform.position.x, -Bounds.yPlayerBound);
        if(transform.position.x > Bounds.xPlayerBound)
        {
            transform.position = leftPos;
        }
        else if(transform.position.x < -Bounds.xPlayerBound)
        {
            transform.position = rightPos;
        }
        else if(transform.position.y > Bounds.yPlayerBound)
        {
            transform.position = botPos;
        }
        else if(transform.position.y < -Bounds.yPlayerBound)
        {
            transform.position = topPos;
        }
        
    }
    private void PlayerShoot()
    {
        Vector2 bulletPos = new Vector2(transform.position.x, transform.position.y + 0.5f);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPlayer, bulletPos, transform.rotation);
            playerAudioSource.PlayOneShot(bulletSound, 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Enemy"))
        {
            AudioClips.playerIsDestroyed = true;
            Destroy(gameObject);
            GameManager.gameOver = true;
        }
       
    }
}
