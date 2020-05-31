using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCollectDown : MonoBehaviour
{
    private Vector2 startPosition;
    private bool triggered;
    private AudioSource breakSound;
    private CameraShake cameraShake;
    
    private void Start()
    {
        startPosition = (Vector2) transform.position;
        breakSound = GetComponent<AudioSource>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            GameControl.instance.PlayerScored(gameObject.tag);
            transform.position = startPosition;
            triggered = true;
            player.AddComplementaryForceDownLeft();
            breakSound.Play();
            StartCoroutine(cameraShake.Shake(.15f, .08f));
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
                //player.AddComplementaryForceDownLeft();
            }
        }
    }
    
    /*
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
                player.IsDownDashing = false;
                player.ReloadDownDashes();
                transform.position = startPosition;
            }
            else
            {
                player.PlayerDied();
            }
        }
    }*/
}
