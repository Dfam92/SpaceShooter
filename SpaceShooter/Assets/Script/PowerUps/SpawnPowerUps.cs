using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    public GameObject powerUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        SpawnRange();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void SpawnRange()
    {
        Vector3 powerUpPos = new Vector3(Random.Range(-CameraEdgesBounds.screenBounds.x, CameraEdgesBounds.screenBounds.x), CameraEdgesBounds.screenBounds.y,gameObject.transform.position.z);
        transform.position = powerUpPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
