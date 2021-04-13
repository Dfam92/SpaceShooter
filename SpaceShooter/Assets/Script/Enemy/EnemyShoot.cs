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
    private void Awake()
    {
        StopEnemyFire();
    }

    private void EnemyFire()
    {
        if(GameManager.isActive == true)
        {
            Instantiate(enemyBullet, transform.position, transform.rotation);
        }
    }

    private void StopEnemyFire()
    {
        if (GameManager.isActive == false)
        {
            CancelInvoke("EnemyFire");
        }
    }

}
