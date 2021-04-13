using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private float turnForce;
    [SerializeField] private float timeToTurn;
    [SerializeField] private float timeToStopTurn;
    [SerializeField] private float turnAngle;
    
    private bool isTurning;
    private float count;
    

    private EnemyControl enemyControl;
    // Start is called before the first frame update
    void Start()
    {
        enemyControl = gameObject.GetComponent<EnemyControl>();
    }
    private void Update()
    {
        if (GameManager.isActive == true)
        {
            count += Time.deltaTime;
            if (count > timeToStopTurn)
            {
                StopCoroutine(Turning());
                isTurning = true;
            }
        }
           
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.isActive == true)
        {
            TurnMovement();
        }
        
        
    }
    private void TurnMovement()
    {
        if (isTurning == false)
        {
            StartCoroutine(Turning());
        }
            
    }
    
    IEnumerator Turning()
    {
        yield return new WaitForSeconds(timeToTurn);
        enemyControl.enemyRb.AddTorque(turnAngle);
        enemyControl.enemyRb.AddForce(Vector2.right * turnForce);
        
    }
    
}
