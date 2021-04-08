using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D bulletRb;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletMovement();
    }

    private void bulletMovement()
    {
        bulletRb.AddForce(Vector2.up, ForceMode2D.Impulse);
        transform.rotation = Quaternion.identity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
