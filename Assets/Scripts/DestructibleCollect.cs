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
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            if (player.IsDashing)
            {
                GameControl.instance.PlayerScored(gameObject.tag);
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
