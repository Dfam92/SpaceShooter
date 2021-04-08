using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemyBullet;
    [SerializeField] private float fireStart;
    [SerializeField] private float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemyFire", fireStart, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameOver == true)
        {
            CancelInvoke("EnemyFire");
        }
    }

    private void EnemyFire()
    {
       Instantiate(enemyBullet, transform.position, transform.rotation);
    }
}
