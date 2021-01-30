﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7;

    public float dashSpeedFactor = 2f;
    public float dashTime = 0.1f;
    public float attackCooldown = 0.01f;

    public Vector2 move;

    public bool unlockedDoubleJump = false;
    public bool unlockedDash = false;
    public bool unlockedAttack = false;


    public bool invincible = false;

    public GameObject AttackArea;

    private bool canDoubleJump = false;
    private bool canAttack = true;
    private bool canDash = true;
    private bool isDashing = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        // velocity cannot be changed while dashing
        if (!isDashing)
        {
            targetVelocity = Vector2.zero;

            if (grounded)
            {
                this.canDash = true;
            }

            if (this.grounded && this.unlockedDoubleJump)
            {
                this.canDoubleJump = true;
            }

            base.ComputeVelocity();
            Vector2 movement = Vector2.zero;

            // horizontal movement
            move.x = Input.GetAxis("Horizontal");

            // vertical movement
            if (Input.GetButtonDown("Jump") && grounded )
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonDown("Jump") && canDoubleJump)
            {
                velocity.y = jumpTakeOffSpeed;
                canDoubleJump = false;
            }

            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            //dash
            if (Input.GetButtonDown("Fire1") && !isDashing && canDash && unlockedDash)
            {
                if (move.x != 0)
                {
                    Dash();
                    StartCoroutine(AfterDash());
                    
                }
                // todo animation
            }

            targetVelocity = move * maxSpeed;

            //attack
            if (Input.GetButtonDown("Fire2") && canAttack && unlockedAttack)
            {
                Attack();
                StartCoroutine(AttackCooldown());
            }
        }

    }

    protected override void FlipSprite()
    {
        bool flipSprite = (spriteRenderer.flipX ? (move.x < -0.01f) : (move.x > 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public void Dash()
    {
        move.x *= dashSpeedFactor;
        invincible = true;
        isDashing = true;
    }

    public void Attack()
    {
        Debug.Log("Attack!");
        canAttack = false;
    }

    IEnumerator AfterDash()
    {
        yield return new WaitForSeconds(dashTime);
        invincible = false;
        isDashing = false;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
