using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject eyelight;

    private bool canDoubleJump = false;
    private bool canAttack = true;
    private bool canDash = true;
    private bool isDashing = false;
    private bool isDying = false;

    private SpriteRenderer spriteRenderer;
    public Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        this.AttackArea.GetComponent<Animator>().SetBool("attack", false);
        // velocity cannot be changed while dashing
        if (!isDashing && !isDying)
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


            animator.SetFloat("vertical_movent", velocity.y);


            //attack
            if (Input.GetButton("Fire2") && canAttack && unlockedAttack)
            {
                Attack();
                StartCoroutine(AttackCooldown());
                ResetAttackAnimation();
            }
        }

    }

    protected override void FlipSprite()
    {
        bool flipSprite = (spriteRenderer.flipX ? (move.x < -0.01f) : (move.x > 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            AttackArea.transform.localPosition = new Vector3(-AttackArea.transform.localPosition.x, AttackArea.transform.localPosition.y, AttackArea.transform.localPosition.z);
            AttackArea.GetComponent<SpriteRenderer>().flipX = !AttackArea.GetComponent<SpriteRenderer>().flipX;
            eyelight.transform.Rotate(180, 0, 0);
        }
        
    }

    public void Dash()
    {
        move.x = Mathf.Sign(move.x) * dashSpeedFactor;
        invincible = true;
        isDashing = true;
        animator.SetBool("isDashing", true);
        canDash = false;
    }

    public void Attack()
    {
        //todo fill
        Debug.Log("Attack!");
        this.AttackArea.GetComponent<Animator>().SetBool("attack", true);
        canAttack = false;
    }

    IEnumerator AfterDash()
    {
        yield return new WaitForSeconds(dashTime);
        invincible = false;
        isDashing = false;
        animator.SetBool("isDashing", false);
    }

    IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(.1f);
        this.AttackArea.GetComponent<Animator>().SetBool("attack", false);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void Damage()
    {
        if (!invincible)
        {
            //todo replace
            animator.SetBool("isDying", true);
            isDying = true;
            StartCoroutine(Die());
        }
        
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            Damage();
        }
        else if (collision.CompareTag("Damage"))
        {
            Damage();
        }
        else if (collision.CompareTag("Goal"))
        {
            GameObject.FindObjectOfType<LevelManager>().ShowNextText();
        }
        else if (collision.CompareTag("Finish"))
        {
            Debug.Log("Reached Finish");
            GameObject.FindObjectOfType<LevelManager>().LoadNextScene();
        }
    }
}
