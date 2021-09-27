using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCase : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D caseRb;
    private bool inCorner;
    [SerializeField] private float speedRotate;
    [SerializeField] private float speedMove;
    // Start is called before the first frame update
    void Start()
    {
        caseRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        StartMove();
        ReverseMove();

    }

    private void StartMove()
    {
        if(GameManager.isActive )
        {
            if(!inCorner)
            {
                var playerFuturPos = new Vector3(ScreenBounds.xPlayerBound, transform.position.y, transform.position.z);
                caseRb.AddTorque(speedRotate);
                transform.position = Vector2.MoveTowards(transform.position, playerFuturPos, speedMove*gameManager.speedIncrease);
                if (transform.position == playerFuturPos)
                {
                    inCorner = true;
                }
            }
            
        }
    }
    private void ReverseMove()
    {
        if(inCorner)
        {
            var playerFuturPos2 = new Vector3(-ScreenBounds.xPlayerBound, transform.position.y, transform.position.z);
            transform.position = Vector2.MoveTowards(transform.position, playerFuturPos2, speedMove);
            caseRb.AddTorque(-speedRotate);
            if (transform.position == playerFuturPos2)
            {
                inCorner = false;
            }
        }
    }
}
