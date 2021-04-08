using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D enemyBulletRb;

    // Start is called before the first frame update
    void Start()
    {
        BulletDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BulletDirection()
    {
        enemyBulletRb.AddForce(Vector2.down, ForceMode2D.Impulse);
        transform.rotation = Quaternion.identity;
    }
}
