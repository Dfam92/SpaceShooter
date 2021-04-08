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
        
    }

    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector2.right * speed * horizontalInput);
        playerRb.AddForce(Vector2.up * speed * verticalInput);
    }
    private void PlayerShoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPlayer, transform.position, transform.rotation);
        }
    }
}
