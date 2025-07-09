using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator playerAnimator;
    public float moveSpeed = 1f;
    public float jumpForce = 1f, jumpFrequency = 1f, nextJumpTime = 0f;
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    bool facingRight = true;
    public bool isGrounded = true;
    void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        OnGroundCheck();
        HorizontalMove();
        if (rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        if (Input.GetAxis("Vertical") > 0 && isGrounded && Time.timeSinceLevelLoad > nextJumpTime)
        {
        nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();

        }
    }
    void FixedUpdate()
    {

    }
    void HorizontalMove()
    {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y);
        playerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(rb.linearVelocity.x));
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));

    }
    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);
playerAnimator.SetBool("IsGrounded", isGrounded);
    
    }
}
