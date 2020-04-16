using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private PolygonCollider2D polygonCollider2D;
    private bool isDead = false;
    [SerializeField]
    private bool onGround;
    private Vector2 startPosition;

    [SerializeField] private LayerMask platformLayerMask;
    
    [Header("Jump Settings")]
    [Range(0,10)]
    public float jumpVelocity = 5;
    public int maxNumJumps;
    [SerializeField]
    private int remainingJumps;
    private BetterJump betterJumpScript;
    
    [Header("Dash Settings")]
    public float dashSpeed;
    public float dragDash;
    private bool isDashing = false;
    public int maxNumDashes;
    [SerializeField]
    private int remainingDashes;

    private bool isReturning = false;

    private Touch touch;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool dashRequest;
    private bool jumpRequest;

    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>(); 
        startPosition = transform.position;
        remainingJumps = maxNumJumps;
        remainingDashes = maxNumDashes;
        betterJumpScript = GetComponent<BetterJump>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Check inputs if player isnt dead
        if(!isDead){
            
            CheckInput();
            
            if(jumpRequest)
            {
                if (remainingJumps > 0)
                {
                    Jump();
                    remainingJumps--;
                }
                jumpRequest = false;
            }

            
            
            if(dashRequest)
            {
                if (remainingDashes > 0)
                {
                    Dash();
                    remainingDashes--;
                }
                dashRequest = false;
            }
            
            /*
            if (IsGrounded())
            {
                ReloadJumps();
                ReloadDashes();
            }
            */

            //Return to original x position if conditions apply

            if (!isReturning)
            {
                if (transform.position.x > startPosition.x && !isDashing && onGround)
                {
                    rb.velocity += Vector2.right * (GameControl.instance.BackgroundScrollSpeed / 2);
                    isReturning = true;
                }
            }
            else
            {
                if (transform.position.x <= startPosition.x || !onGround)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    isReturning = false;
                }
                else
                {
                    rb.velocity = new Vector2(GameControl.instance.BackgroundScrollSpeed / 2, rb.velocity.y);
                }
            }
            
        }
        
    }

    private void FixedUpdate()
    {
        if (onGround)
        {
            ReloadDashes();
            ReloadJumps();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    }
    
    
    private void Dash()
    {
        //rb.velocity = Vector2.zero;
        rb.velocity = Vector2.right * dashSpeed;
        rb.drag = 10;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        StartCoroutine(ReduceDragDash(.4f));
        
        rb.gravityScale = 0;
        betterJumpScript.enabled = false;
        isDashing = true;
        isReturning = false;
        
        yield return new WaitForSeconds(.3f);
        
        rb.gravityScale = 2f;
        betterJumpScript.enabled = true;
        isDashing = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    IEnumerator ReduceDragDash(float duration)
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            rb.drag = Mathf.Lerp(dragDash, 0, counter/duration);
            yield return null;
        }
        
    }

    /*
    private bool IsGrounded()
    {
        float heightTest = .00875f;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(polygonCollider2D.bounds.center, polygonCollider2D.bounds.size,
            0f, Vector2.down, heightTest, platformLayerMask);
        return raycastHit2D.collider != null;
    }*/
    

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            ReloadJumps();
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            ReloadDashes();
        }
    }

    private void ReloadJumps()
    {
        remainingJumps = maxNumJumps;
    }

    public void ReloadDashes()
    {
        remainingDashes = maxNumDashes;
    }

    private void CheckInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;

                    if (startTouchPosition == endTouchPosition)
                    {
                        jumpRequest = true;
                    }
                    else if (startTouchPosition.x < endTouchPosition.x)
                    {
                        dashRequest = true;
                    }

                    break;
            }
        }
    }

    public bool IsDashing
    {
        get => isDashing;
        set => isDashing = value;
    }

    public void PlayerDied()
    {
        rb.velocity = Vector2.zero;
        //sprite
        isDead = true;
        GameControl.instance.PlayerDied();
    }
}
