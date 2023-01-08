using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float moveSpeed = 10f;

    public float jumpForce = 10f;

    bool isJumping = false;

    bool isOnGround;

    bool canDoubleJump;

    public bool isDoubleJumpActive;

    public LayerMask groundLayer;

    public Transform groundPoint;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isOnGround =
            Physics2D.OverlapCircle(groundPoint.position, 0.2f, groundLayer);
        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || canDoubleJump))
        {
            isJumping = true;

            if (isOnGround)
            {
                if (isDoubleJumpActive)
                {
                    canDoubleJump = true;
                }
            }
            else
            {
                canDoubleJump = false;
                anim.SetTrigger("doubleJump");
            }
        }
    }

    //Put physics based movement in here
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = Vector3.one;

            rb2d.AddForce(new Vector2(moveSpeed, 0), ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddForce(new Vector2(-moveSpeed, 0), ForceMode2D.Impulse);

            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (isJumping)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }
    }
}
