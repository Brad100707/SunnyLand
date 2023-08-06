using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagle : Enemy
{
    public Transform Top, Bottom;
    float TOP_y, Bottom_y, Middle_y;
    Rigidbody2D rb;
    ///Animator anim;
    public float Speed;
    bool Fly = true;
    protected override void Start()
    {
       base.Start();
        TOP_y = Top.position.y;
        Bottom_y = Bottom.position.y;
        ///Destroy(Top.gameObject);
        ///Destroy(Bottom.gameObject);
        rb = GetComponent<Rigidbody2D>();
        ///anim = GetComponent<Animator>();
        Speed = Speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Fly)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > TOP_y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -Speed);
                Fly = false;
            }
        }
        if (!Fly)
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < Bottom_y)
            {
                rb.velocity = new Vector2(rb.velocity.x, Speed);
                Fly = true;
            }
        }

    }


}
