using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRoll : MonoBehaviour
{
    private Rigidbody2D backRb;
    [SerializeField] private float rollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        backRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        backRb.AddForce(Vector2.down * rollSpeed);
    }
}
