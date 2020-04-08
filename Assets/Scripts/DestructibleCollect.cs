using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCollect : MonoBehaviour
{

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = (Vector2) transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();
        
        if (player != null)
        {
            if (player.IsDashing)
            {
                transform.position = startPosition;
                player.IsDashing = false;
            }
            else
            {
                GameControl.instance.PlayerDied();
            }
        }
    }
}
