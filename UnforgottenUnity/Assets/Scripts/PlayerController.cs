using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7;

    public float dashSpeedFactor = 2f;
    public float dashTime = 0.1f;

    public Vector2 move;

    public bool unlockedDoubleJump = false;
    public bool unlockedDash = false;


    public bool invincible = false;

    private bool canDoubleJump = false;
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
            if (Input.GetButtonDown("Fire1") && !isDashing && unlockedDash)
            {
                //if (move.x != 0)
                {
                    move.x *= dashSpeedFactor;
                    invincible = true;
                    StartCoroutine(AfterDash());
                    isDashing = true;
                }
                // todo animation
            }

            targetVelocity = move * maxSpeed;
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

    }
    IEnumerator AfterDash()
    {
        yield return new WaitForSeconds(dashTime);
        invincible = false;
        isDashing = false;
    }
}
