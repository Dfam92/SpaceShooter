using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public static float fireStart = 6;
    public static float fireRate = 30;

    // Start is called before the first frame update
    void Start()
    {
        float randomFire = Random.Range(fireStart, fireRate);
        InvokeRepeating("EnemyFire", randomFire, randomFire);
        
    }
    private void Awake()
    {
        StopEnemyFire();
    }

    public virtual void EnemyFire()
    {
        if(GameManager.isActive == true)
        {
            //Instantiate(enemyBullet, transform.position, transform.rotation);
            ObjectPooler.Instance.SpawnFromPool("EnemyBullet1", transform.position, Quaternion.identity);
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
