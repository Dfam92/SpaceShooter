using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSting : MonoBehaviour
{
    public Rigidbody2D stingRb;
    private PlayerControl player;

    [SerializeField]private float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StingDirection();
    }

    private void StingDirection()
    {
        stingRb.AddForce(Vector2.LerpUnclamped(transform.position,player.transform.position,speed));
        
    }
}
