using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Shoot : EnemyShoot
{
    public override void EnemyFire()
    {
        if (GameManager.isActive == true)
        {
            //Instantiate(enemyBullet, transform.position, transform.rotation);
            ObjectPooler.Instance.SpawnFromPool("EnemyBullet2", transform.position, transform.rotation);
        }
    }
}
