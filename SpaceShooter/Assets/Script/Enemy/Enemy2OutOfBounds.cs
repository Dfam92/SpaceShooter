using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2OutOfBounds : MonoBehaviour
{
    private float yRestartPos = 4;
    private float yStartPos;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Vector2 startPos = new Vector2(-ScreenBounds.xEnemyBound, transform.position.y-1);
        Vector2 topPos = new Vector2(-ScreenBounds.xEnemyBound, ScreenBounds.yEnemyBound);
        

        if (collision.CompareTag("RightBound"))
        {
            transform.position = startPos;
           
        }
        else if (collision.CompareTag("BotBound"))
        {
            gameObject.transform.position = topPos;
            yRestartPos = ScreenBounds.yEnemyBound;
        }
    }
}
