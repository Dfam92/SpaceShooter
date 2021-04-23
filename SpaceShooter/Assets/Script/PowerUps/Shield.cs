using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]private static int shieldHealth = 3;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        ShieldColor();
    }

    public static void ShieldHit()
    {
        shieldHealth -= 1;
    }

    private void ShieldColor()
    {
        
        if (shieldHealth == 2)
        {
            spriteRenderer.color = Color.green;
        }
        else if (shieldHealth == 1)
        {
            spriteRenderer.color = Color.red;
        }
        else if (shieldHealth < 1)
        {
            gameObject.SetActive(false);
            shieldHealth = 3;
            spriteRenderer.color = defaultColor;
        }
    }
}
