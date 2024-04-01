using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Transform player;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private bool canMove = true;
    public Transform shootPoint;
    private float bulletSpeed = 2000f;
    private float fireRate = 0.75f;
    private bool readyToShoot = true;
    private float sightRange = 15f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (checkIfAlive())
        {
            if (!playerInSightrange())
            {
                if(canMove)
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
            }
            else
            {
                animator.SetFloat("Speed", 0f);
                rb.velocity = Vector2.zero;
                if (transform.position.x > player.position.x)
                {
                    Rotate(true);
                }
                else
                {
                    Rotate(false);
                }
                if (readyToShoot)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
        else
        {
            Die();
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.right * bulletSpeed);
        readyToShoot = false;
        yield return new WaitForSeconds(fireRate);
        readyToShoot = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private bool checkIfAlive()
    {
        if(GetComponentInChildren<EnemyStats>().HP > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool playerInSightrange()
    {
        return Physics2D.OverlapBox(transform.position, new Vector2(sightRange, 1.7f), 0f, playerLayer);
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
            if(bulletSpeed > -Mathf.Epsilon)
            {
                bulletSpeed = -bulletSpeed;
            }

        }
        else
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), transform.localScale.y);
            bulletSpeed = Math.Abs(bulletSpeed);
        }
    }
}
