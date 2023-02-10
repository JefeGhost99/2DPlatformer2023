using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed;

    private Rigidbody2D theRB;

    public float jumpForce;

    private bool isGrounded;

    public Transform groundCheckPoint;

    public LayerMask whatIsGround;

    private SpriteRenderer theSR;

    private bool canDoubleJumpt;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(moveSpeed* Input.GetAxis("Horizontal"), theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                canDoubleJumpt = true;
            }
            else
            {
                if (canDoubleJumpt)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    canDoubleJumpt = false;
                }
            }
            
        }
        if (theRB.velocity.x < 0)
        {
            theSR.flipX = false;
        }
        else if (theRB.velocity.x > 0)
        {
            theSR.flipX = true;
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

    }
}
