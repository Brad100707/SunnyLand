using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy
{
    Rigidbody2D rb;
    Collider2D coll;
    public LayerMask Ground;
    ///Animator anim;
    public Transform LeftPoint, RightPoint;
    private bool Faceleft = true;
    public float Speed, JumpForce;
    float LeftX, RightX;
   
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Speed = Speed * Time.deltaTime;
        JumpForce = JumpForce * Time.deltaTime;
        transform.DetachChildren();
        LeftX = LeftPoint.position.x;
        RightX = RightPoint.position.x;
        ///Destroy(LeftPoint.gameObject);
        ///Destroy(RightPoint.gameObject);
        coll = GetComponent<Collider2D>();
        ///anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if(Faceleft)
        {
            if (coll.IsTouchingLayers(Ground))
            {

                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if (transform.position.x < LeftX) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(Ground))
            {

                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
            if (transform.position.x > RightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    void SwitchAnim()
    { 
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1f)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if(coll.IsTouchingLayers(Ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }


}
