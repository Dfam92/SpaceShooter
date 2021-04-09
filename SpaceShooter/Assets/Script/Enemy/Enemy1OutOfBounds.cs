using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 topPos = new Vector2(transform.position.x, Bounds.yEnemyBound);
        Vector2 rightPos = new Vector2(Bounds.xBound,transform.position.y);
        Vector2 leftPos = new Vector2(-Bounds.xBound, transform.position.y);
        Vector2 botPos = new Vector2(transform.position.x, -Bounds.yEnemyBound);

        if (collision.CompareTag("BotBound"))
        {
            gameObject.transform.position = topPos;
        }
        else if(collision.CompareTag("LeftBound"))
        {
            gameObject.transform.position = rightPos;
        }
        else if(collision.CompareTag("RightBound"))
        {
            gameObject.transform.position = leftPos;
        }
        else if(collision.CompareTag("TopBound"))
        {
            gameObject.transform.position = botPos;
        }
    }
}
