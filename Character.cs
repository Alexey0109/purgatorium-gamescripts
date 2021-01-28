﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*       Version update by Keyboard Destroyer
*       TODO
*       1. Fix Jump function    (Done)
*       2. Add coliders check   (Done)
*       3. Add Trigger check    (Done)
*       4. Damage & Enemies     (Enemy tag added)
*/


public class Character : MonoBehaviour
{

    [SerializeField]  private Transform transformPosition;
    [SerializeField]  private LayerMask whatIsGround;


    private new Rigidbody2D     rigidbody;
    private     SpriteRenderer  spriteRenderer;

    private const float speed       =   5f;
    private const float jumpForce   =   5f;
    private const float checkRadius =   .2f;
    private       float moveInput;
    
    private       bool isGrounded;
    
    void Start()
    {
        rigidbody       = GetComponent<Rigidbody2D>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        //I used OnCollisionEnter2D function to check ground. *shrug* :D

        //isGrounded = Physics2D.OverlapCircle(transformPosition.position, checkRadius, whatIsGround);

        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W)))
        {
            Jump(); //Do jump
        }
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(moveInput * speed, rigidbody.velocity.y);
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)                   //If we have trigger
    {
        Debug.Log("Apply damage! Trigger entered!");        
        if (col.gameObject.tag == "enemy")                  //If we touched an enemy
        {
            Debug.Log("You died!");                         //Apply some hp damage
            /*Do smth here*/
        }
    }

    void OnCollisionEnter2D(Collision2D col)                //If we have collision
    {
        //Debug.Log("Apply Physics! Collision entered!");
        if(col.gameObject.tag == "ground")                  //If we're standing on ground
            isGrounded = true;                              //bool isGrounded should be true so that we can jump
    }

    public void Jump() //Not used
    {
        rigidbody.velocity = Vector2.up * jumpForce;        //Apply jump force
        isGrounded = false;                                 //We're not grounded now
    }
}