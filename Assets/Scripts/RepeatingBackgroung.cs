using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackgroung : MonoBehaviour
{
    //private BoxCollider2D groundCollider;
    private SpriteRenderer spriteRenderer;
    private float groundHorizontalLength;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength =  spriteRenderer.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -groundHorizontalLength)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2) transform.position + groundOffset;
    }
    
}    
