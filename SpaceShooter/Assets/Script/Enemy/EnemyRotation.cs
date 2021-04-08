using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private float turnForce;
    [SerializeField] private int timeToTurn;
    [SerializeField] private float turnAngle;

    private EnemyControl enemyControl;
    // Start is called before the first frame update
    void Start()
    {
        enemyControl = gameObject.GetComponent<EnemyControl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void TurnMovement()
    {
        StartCoroutine(Turning());
    }
    IEnumerator Turning()
    {
        yield return new WaitForSeconds(timeToTurn);
        enemyControl.enemyRb.AddTorque(turnAngle);
        enemyControl.enemyRb.AddForce(Vector2.right * turnForce);
    }
}
