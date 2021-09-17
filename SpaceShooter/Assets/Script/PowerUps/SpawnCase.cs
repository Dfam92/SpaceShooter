using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCase : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Start()
    {
        var randomPos = new Vector2(Random.Range(-ScreenBounds.xPlayerBound + 0.5f, ScreenBounds.xPlayerBound - 0.5f), ScreenBounds.yPlayerBound + 0.75f);
        transform.position = randomPos;
    }
}
