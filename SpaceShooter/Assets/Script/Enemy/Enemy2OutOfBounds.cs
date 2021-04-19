using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2OutOfBounds : MonoBehaviour
{
    private float yRestartPos = 4;
    private float yStartPos;

    private void LateUpdate()
    {
        OutOfBounds();
    }
   void OutOfBounds()
    {
        Vector2 startPos = new Vector2(-ScreenBounds.xEnemyBound, transform.position.y - 1);
        Vector2 topPos = new Vector2(-ScreenBounds.xEnemyBound, ScreenBounds.yEnemyBound);


        if (transform.position.x > ScreenBounds.xEnemyBound+0.5f)
        {
            transform.position = startPos;

        }
        else if (transform.position.y < -ScreenBounds.yEnemyBound)
        {
            gameObject.transform.position = topPos;
            yRestartPos = ScreenBounds.yEnemyBound;
        }
    }
        
    
}
