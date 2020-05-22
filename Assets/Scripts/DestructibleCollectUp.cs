using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCollectUp : MonoBehaviour
{
    private Vector2 startPosition;
    private AudioSource breakSound;
    //private bool triggered;

    private void Start()
    {
        startPosition = (Vector2) transform.position;
        breakSound = GetComponent<AudioSource>();
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null && player.IsDownDashing)
        {
            player.IsDownDashing = false;
            player.ReloadDownDashes();
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
    }*/
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            player.PlayerDied();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            if (player.IsDownDashing)
            {
                GameControl.instance.PlayerScored(gameObject.tag);
                player.IsDownDashing = false;
                player.ReloadDownDashes();
                player.AddComplementaryForceUpLeft();
                transform.position = startPosition;
                breakSound.Play();
            }
            else
            {
                player.PlayerDied();
            }
        }
    }
}
