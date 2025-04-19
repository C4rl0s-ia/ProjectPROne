using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] ManagerInputs managerInput;
    [SerializeField] Rigidbody2D rigidBody2D;
    [SerializeField] Detection detection;

    [Header("Physics")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float fallgravity;
    [SerializeField] float Jumpgravity;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.3f;
    private float coyoteTimerCounter;
    private bool canCoyoteTime;

    [SerializeField] bool isFacingRight = true;


    private float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        managerInput.OneButtonEvent += ManagerInputs_OneButtonEvent;
        gravityScale = rigidBody2D.gravityScale;
    }

    private void Update()
    {
        Coyote(detection.ground != null);
        CheckDirection();
    }
    private void ManagerInputs_OneButtonEvent()
    {
        if (detection.ground != null || canCoyoteTime)
        {
            JumpPlayer();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        JumpBetterPlayer();
    }

    private void Move()
    {
        rigidBody2D.linearVelocity = new Vector2(managerInput.GetInputX() * speed, rigidBody2D.linearVelocity.y);
    }

    private void CheckDirection()
    {
        if (isFacingRight && managerInput.GetInputX() < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && managerInput.GetInputX() > 0f)
        {
            Flip();
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void JumpPlayer()
    {
        rigidBody2D.linearVelocity = Vector2.up * jumpForce;
    }


    private void JumpBetterPlayer()
    {
        if (rigidBody2D.linearVelocity.y < 0)
        {
            rigidBody2D.gravityScale = fallgravity;
        }
        else if (rigidBody2D.linearVelocity.y > 0 && !managerInput.IsDown())
        {
            rigidBody2D.gravityScale = Jumpgravity;
        }
        else
        {
            rigidBody2D.gravityScale = gravityScale;
        }
    }


    private void Coyote(bool coyote)
    {
        if (coyote)
        {
            coyoteTimerCounter = 0f;
            canCoyoteTime = true;
        }
        if (detection.ground == null && canCoyoteTime)
        {
            coyoteTimerCounter += Time.deltaTime;
            if(coyoteTimerCounter > coyoteTime)
            {
                canCoyoteTime = false;
            }
        }

    }

}