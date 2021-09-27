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
    private void Update()
    {
        if (transform.position.y < -ScreenBounds.yPlayerBound - 2f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet") || collision.CompareTag("Player"))
        {
            StartCoroutine(Desactive());
            Instantiate(explodeCase, this.transform.position, Quaternion.identity);
            StartCoroutine(SpawnTime());
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            
        }
    }
    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(timeToSpawnPowerUp);
        int index = Random.Range(0, powerUps.Count);
        Instantiate(powerUps[index], this.transform.position, Quaternion.identity);
    }

    IEnumerator Desactive()
    {
        yield return new WaitForSeconds(3);
        spriteRenderer.enabled = true;
        circleCollider.enabled = true;
        this.gameObject.SetActive(false);

    }
}
