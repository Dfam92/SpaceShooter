using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseDestroyed : MonoBehaviour
{
    public List<GameObject> powerUps;
    public GameObject explodeCase;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    [SerializeField] private float timeToSpawnPowerUp;
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
            Instantiate(explodeCase, this.transform.position, Quaternion.identity);
            StartCoroutine(SpawnTime());
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            Destroy(this.gameObject, 3);
        }
    }
    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(timeToSpawnPowerUp);
        int index = Random.Range(0, powerUps.Count);
        Instantiate(powerUps[index], this.transform.position, Quaternion.identity);
    }
}
