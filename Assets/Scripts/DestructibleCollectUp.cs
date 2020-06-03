using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCollectUp : MonoBehaviour
{
    private Vector2 startPosition;
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
                StartCoroutine(cameraShake.Shake(.15f, .08f));
            }
            else
            {
                player.PlayerDied();
            }
        }
    }
}
