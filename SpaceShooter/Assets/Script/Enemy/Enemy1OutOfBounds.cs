using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1OutOfBounds : MonoBehaviour
{
    private void LateUpdate()
    {
        
       OutOfBounds();
        
    }
    void OutOfBounds()
    {
        Vector2 topPos = new Vector2(transform.position.x, ScreenBounds.yEnemyBound);
        Vector2 rightPos = new Vector2(ScreenBounds.xEnemyBound, transform.position.y);
        Vector2 leftPos = new Vector2(-ScreenBounds.xEnemyBound, transform.position.y);
        Vector2 botPos = new Vector2(transform.position.x, -ScreenBounds.yEnemyBound);

        if (transform.position.y < -ScreenBounds.yEnemyBound-0.5f)
        {
            gameObject.transform.position = topPos;
        }
        if (transform.position.x < -ScreenBounds.xEnemyBound-0.5f)
        {
            gameObject.transform.position = rightPos;
        }
        else if (transform.position.x > ScreenBounds.xEnemyBound + 0.5f)
        {
            gameObject.transform.position = leftPos;
        }
        /*else if (transform.position.y > ScreenBounds.yEnemyBound + 0.5f)
        {
            gameObject.transform.position = botPos;
        }*/
    }

    

}
