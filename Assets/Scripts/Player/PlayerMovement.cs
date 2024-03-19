using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    private Rigidbody2D rb;
    private float horizontal;
    private float speed = 3;
    private float sprintSpeed = 3;
    private float jumpHeight = 10;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       move();
    }

    private void move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        if (Input.GetKey(KeyCode.LeftShift)){
            movement = new Vector2(horizontal * speed * sprintSpeed, rb.velocity.y);
        }
        else
        {
            movement = new Vector2 (horizontal * speed, rb.velocity.y);
        }
        rb.velocity = movement;

        rb.AddForce(new Vector2 (0, -1));
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(new Vector2(transform.position.x, transform.position.y - 1.5f), new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal, 0, layerMask);
    }
}
