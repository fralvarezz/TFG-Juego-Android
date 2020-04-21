using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCollectDown : MonoBehaviour
{
    private Vector2 startPosition;
    private bool triggered;

    private void Start()
    {
        startPosition = (Vector2) transform.position;
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            transform.position = startPosition;
            triggered = true;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            if (!triggered)
            {
                player.PlayerDied();
            }
            else
            {
                triggered = false;
            }
        }
    }
}
