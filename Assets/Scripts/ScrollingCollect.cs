using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCollect : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float topScreen;
    public float bottomScreen;
    public float yVelocity;
    public float xVelocityModificator;
    private float halfScreen;

    // Use this for initialization
    void Start () 
    {
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();

        //Start the object moving.
        halfScreen = (Mathf.Abs(topScreen + Mathf.Abs(bottomScreen))) / 2;
        bool random = (Random.value > 0.5f);

        if (random)
        {
            rb2d.velocity = new Vector2 (GameControl.instance.BackgroundScrollSpeed * xVelocityModificator, yVelocity);
        }
        else
        {
            rb2d.velocity = new Vector2 (GameControl.instance.BackgroundScrollSpeed * xVelocityModificator, -yVelocity);
        }
    }

    void FixedUpdate()
    {
        // If the game is over, stop scrolling.
        if(GameControl.instance.gameOver)
        {
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            if (transform.position.y >= topScreen)
            {
                rb2d.velocity = new Vector2 (GameControl.instance.BackgroundScrollSpeed * xVelocityModificator, -rb2d.velocity.y);
            }
            else if (transform.position.y <= bottomScreen)
            {
                rb2d.velocity = new Vector2 (GameControl.instance.BackgroundScrollSpeed * xVelocityModificator, -rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2 (GameControl.instance.BackgroundScrollSpeed * xVelocityModificator, rb2d.velocity.y);
            }

        }
    }
}
