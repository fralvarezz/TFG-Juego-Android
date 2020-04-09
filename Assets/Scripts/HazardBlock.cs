using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBlock : MonoBehaviour
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
            player.PlayerDied();
        }
    }
}
