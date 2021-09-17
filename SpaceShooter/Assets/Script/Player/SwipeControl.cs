using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private Vector3 touchPosition;
    private Rigidbody2D playerRb;
    private Vector3 direction;
    [SerializeField]private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            playerRb.velocity = new Vector2(direction.x, direction.y)*moveSpeed;

            if(touch.phase == TouchPhase.Ended)
            {
                playerRb.velocity = Vector2.zero;
            }
        }
    }
}
