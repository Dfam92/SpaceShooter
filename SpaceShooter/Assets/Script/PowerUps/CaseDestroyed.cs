using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseDestroyed : MonoBehaviour
{
    public List<GameObject> powerUps;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            int index = Random.Range(0, powerUps.Count);
            Instantiate(powerUps[index],this.transform.position,Quaternion.identity);
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            Destroy(this.gameObject, 3);
        }
    }
}
