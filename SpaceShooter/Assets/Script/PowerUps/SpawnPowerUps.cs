using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
        
    // Start is called before the first frame update
   
    private void Awake()
    {
        SpawnRangeX();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -ScreenBounds.yPlayerBound-1f)
        {
            Destroy(this.gameObject);
        }

    }

    private void SpawnRangeX()
    {
        Vector3 powerUpPos = new Vector3(Random.Range(-CameraEdgesBounds.screenBounds.x+0.5f, CameraEdgesBounds.screenBounds.x-0.5f), CameraEdgesBounds.screenBounds.y+0.5f,gameObject.transform.position.z);
        transform.position = powerUpPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("AlienShield"))
        {
            Destroy(this.gameObject);
        }
        
    }

}
