using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackGround : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.y / 2;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < startPos.y - repeatWidth)
        {
            transform.position = startPos;
            spriteRenderer.sortingOrder = -2;
        }
    }
}
