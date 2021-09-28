using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRoll : MonoBehaviour
{
    private Rigidbody2D backRb;
    public float rollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        backRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {if(GameManager.isActive)
        {
           backRb.AddForce(Vector2.down * rollSpeed);
        }
    }

    public void StopBackGround()
    {
        rollSpeed = 0;
    }
    public void ContinueBackGround()
    {
        rollSpeed = 0.3f;
    }
}
