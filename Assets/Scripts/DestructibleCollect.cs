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
        /*foreach (Transform child in transform)
        {
            if (child.name == "Platform")
            {
                platform = child.GetComponent<BoxCollider2D>();
            }
            else if (child.name == "FaceHitbox")
            {
                face = child.GetComponent<BoxCollider2D>();
            }
        }*/
    }

    /*
    private void OnCollisionEnter2D(Collision2D other)
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
    */
    
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
