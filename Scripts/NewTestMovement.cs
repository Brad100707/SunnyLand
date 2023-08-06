using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTestMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;
    public float jumpForce, Speed;

    public Transform GroundCheck;
    public LayerMask Ground;

    public bool isJump, isGround;

    bool JumpPressed;
    int JumpCount;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && JumpCount>0)
        {
            JumpPressed = true;
            
        }
    }

    private void FixedUpdate()
    {
        GroundMovement();
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, Ground);
        Jump();
    }

    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * Speed, rb.velocity.y);

        if(horizontalMove!=0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void Jump()
    {
        if(isGround)
        {
            JumpCount = 2;
            isJump = false;
        }
        if(JumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            JumpCount--;
            JumpPressed = false;
        }
        else if(JumpPressed && JumpCount>0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            JumpCount--;
            JumpPressed = false;
        }
    }
}
