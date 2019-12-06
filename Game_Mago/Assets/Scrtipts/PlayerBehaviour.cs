using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Animator playerAnimator;

    //Moviment
    public float speed;
    public float maxSpeed;
    public float speedJump;
    public float maxspeedJump;
    private Rigidbody2D rigidbodyPlayer;
    private bool looktoRight = true;
    private Vector3 positionRight;
    private Vector3 positionLeft;
    private bool isGround = false;
    public Transform GroundCheck;
    public LayerMask WhatisGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        positionRight = transform.localScale;
        positionLeft = positionRight;
        positionLeft.x *= -1;

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 inputDirection = new Vector2 (Input.GetAxis("Horizontal")*speed,0);

        if (inputDirection.x > 0)
            looktoRight = true;

        if (inputDirection.x < 0)
            looktoRight = false;


        if (looktoRight)
        {
            transform.localScale = positionRight;
        }
        else
        {
            transform.localScale = positionLeft;
        }


        rigidbodyPlayer.velocity = new Vector2(inputDirection.x,rigidbodyPlayer.velocity.y);

        if (rigidbodyPlayer.velocity.x > maxSpeed)
        {
            rigidbodyPlayer.velocity = new Vector2(maxSpeed, rigidbodyPlayer.velocity.y);
        }

        if (rigidbodyPlayer.velocity.x < -maxSpeed)
        {
            rigidbodyPlayer.velocity = new Vector2(-maxSpeed, rigidbodyPlayer.velocity.y);
        }

        playerAnimator.SetFloat("velocity",Mathf.Abs(inputDirection.x));


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Physics2D.Linecast (transform.position,GroundCheck.position, WhatisGround))
        {
            isGround = true;

        }else
        {
            isGround = false;
        }
    }

    public void Jump()
    {
        if (isGround)
        {
            rigidbodyPlayer.AddForce(new Vector2(0, speedJump));
            playerAnimator.SetTrigger("Jump");
        }
    }
}
