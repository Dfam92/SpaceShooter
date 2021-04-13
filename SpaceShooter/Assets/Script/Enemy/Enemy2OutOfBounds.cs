using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2OutOfBounds : MonoBehaviour
{
    private float yPos = 4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 leftPos = new Vector2(-ScreenBounds.xEnemyBound, yPos);
        Vector2 topPos = new Vector2(-ScreenBounds.xEnemyBound, ScreenBounds.yEnemyBound);
        if (collision.CompareTag("RightBound"))
        {
            gameObject.transform.position = leftPos;
            yPos += -1;
        }
        else if (collision.CompareTag("BotBound"))
        {
            gameObject.transform.position = topPos;
            yPos = ScreenBounds.yEnemyBound;
        }
    }
}
