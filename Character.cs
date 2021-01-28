using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    [SerializeField]  private Transform transformPosition;
    [SerializeField]  private LayerMask whatIsGround;

    [SerializeField]  private string reloadScene;

    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private const float speed = 5f;
    private const float jumpForce = 7f;
    private const float checkRadius = .2f;
    private float moveInput;
    
    private bool isGrounded;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(moveInput * speed, rigidbody.velocity.y);
        if (moveInput > 0)
        {
            anim.SetBool("isRunning", true);
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            anim.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
        } else
        {
            anim.SetBool("isRunning", false);
        }

        
        isGrounded = Physics2D.OverlapCircle(transformPosition.position, checkRadius, whatIsGround);

        if (isGrounded && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
        {
            rigidbody.velocity = Vector2.up * jumpForce;
        }

        if (!isGrounded && Input.GetKey(KeyCode.W) && rigidbody.velocity.y < 0)
        {
            rigidbody.gravityScale = 0.25f;
        }
        else
        {
            rigidbody.gravityScale = 1f;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rigidbody.velocity = Vector2.up * jumpForce;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemies")
        {
            SceneManager.LoadScene(reloadScene);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemies")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
