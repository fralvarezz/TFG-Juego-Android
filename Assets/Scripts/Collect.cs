using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private Vector2 startPosition;

    private AudioSource pickUp;
    
    private void Start()
    {
        startPosition = (Vector2) transform.position;
        pickUp = GetComponent<AudioSource>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            transform.position = startPosition;
            GameControl.instance.PlayerScored(gameObject.tag);
            pickUp.Play();
        }
    }
}
