using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    private Rigidbody2D rb;
    private float horizontal;
    private float speed = 4f;
    private float sprintSpeed = 1.5f;
    private float jumpHeight = 8f;
    private Vector2 movement;
    private int jumps = 0;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        AnimationController();
    }

    private void move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (jumps < 1)
            {
                jumps++;
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                animator.SetTrigger("Jumped");
            }

        }
        if (isGrounded())
        {
            jumps = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift)){
            movement = new Vector2(horizontal * speed * sprintSpeed, rb.velocity.y);
        }
        else
        {
            movement = new Vector2 (horizontal * speed, rb.velocity.y);
            
        }
        rb.AddForce(new Vector2(0, -1));
        rb.velocity = movement;
    }

    private void AnimationController()
    {
        if (rb.velocity.x != 0f)
        {
            animator.SetInteger("Speed", 2);
        }
        else if(rb.velocity.x == 0f)
        {
            animator.SetInteger("Speed", 0);
        }
        if (rb.velocity.y != 0f)
        {
            animator.SetInteger("VelocityY", 2);
        }
        if(rb.velocity.y == 0f)
        {
            animator.SetInteger("VelocityY", 0);
        }
    }

    private bool isGrounded()
    {

        return Physics2D.OverlapCapsule(new Vector2(transform.position.x, transform.position.y - 1.5f), new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal, 0, layerMask);
    }
}
