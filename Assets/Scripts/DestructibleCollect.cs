using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCollect : MonoBehaviour
{

    private Vector2 startPosition;
    //private BoxCollider2D platform;
    //private BoxCollider2D face;

    private void Start()
    {
        startPosition = (Vector2) transform.position;
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            if (player.IsDashing)
            {
                transform.position = startPosition;
                player.IsDashing = false;
                player.ReloadDashes();
            }
            else
            {
                player.PlayerDied();
            }
        }
    }
}
