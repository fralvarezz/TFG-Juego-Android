using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Vector2 startPosition; //position where the object starts
    public float scrollOffset;  //how much to go forward until it is repositioned
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = (Vector2)transform.position - (Vector2.right * scrollOffset);
    }

    // Update is called once per frame
    void Update()
    {
        //if game isnt ended yet, floor moves
        if (!GameControl.instance.gameOver)
        {
            float newPosition = Mathf.Repeat(Time.time * GameControl.instance.backgroundScrollSpeed, scrollOffset);
            transform.position = startPosition + Vector2.right * newPosition;
        }
    }
}
