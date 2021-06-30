using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public UnityEvent SpawningBoss;
    public UnityEvent DefeatedBoss;
    // Start is called before the first frame update
    
    private void BossAlive()
    {
        SpawningBoss.Invoke();
    }

    private void BossDefeated()
    {
        DefeatedBoss.Invoke();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
