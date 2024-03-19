using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    Rigidbody2D rb;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Transform player;
    private float sightRange = 15f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!playerInSightrange())
        {
            if (isFacingRight())
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            }
            if (rb.velocity.magnitude > Mathf.Epsilon)
            {
                animator.SetFloat("Speed", 0.12f);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            rb.velocity = Vector2.zero;
            if(transform.position.x > player.position.x)
            {
                Rotate(true);
            }
            else
            {
                Rotate(false);
            }
        }
    }

    private bool playerInSightrange()
    {
        return Physics2D.OverlapBox(transform.position, new Vector2(sightRange, 1.7f), 0f, EnemyStats.instance.playerLayer);
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rotate(true);
    }

    private void Rotate(bool invert)
    {
        if (invert)
        {
            transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), transform.localScale.y);
        }
    }
}
