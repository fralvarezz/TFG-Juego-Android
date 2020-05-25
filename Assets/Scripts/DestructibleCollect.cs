using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCollect : MonoBehaviour
{
    private AudioSource breakSound;
    private Vector2 startPosition;

    private bool triggered;

    private void Start()
    {
        startPosition = (Vector2) transform.position;
        breakSound = GetComponent<AudioSource>();
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
                triggered = true;
                player.IsDashing = false;
                player.AddComplementaryForceUpLeft();
                player.ReloadDashes();
                breakSound.Play();
            }
            else
            {
                player.PlayerDied();
            }
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
                player.AddComplementaryForceDownLeft();
            }
        }
    }
}
