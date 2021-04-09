using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    public Rigidbody2D playerRb;
    public GameObject bulletPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void PlayerOutBounds()
    {
        Vector2 topPos = new Vector2(transform.position.x, Bounds.yPlayerBound);
        Vector2 rightPos = new Vector2(Bounds.xBound, transform.position.y);
        Vector2 leftPos = new Vector2(-Bounds.xBound, transform.position.y);
        Vector2 botPos = new Vector2(transform.position.x, -Bounds.yPlayerBound);
        if(transform.position.x > Bounds.xBound)
        {
            transform.position = leftPos;
        }
        else if(transform.position.x < -Bounds.xBound)
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            GameManager.gameOver = true;
        }
       
    }
}
