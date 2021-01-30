using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public float idleTime;
    public Animator animator;
    public int maxHealth;
    public Image healthBarFill;

    private enum BossState
    {
        Spawning,
        Idle,
        WaitingForAttack,
        Attacking
    }

    private enum BossAttack
    {
        None,
        Puppet,
        Clap,
        Smash
    }

    private BossState state;
    private BossAttack lastAttack;
    private float nextAttackTime;
    private int health;


    private void Start()
    {
        state = BossState.Spawning;
        lastAttack = BossAttack.None;
        health = maxHealth;
    }

    private void Update()
    {
        switch (state)
        {
            case BossState.Spawning:
                break;
            case BossState.Idle:
                if (Time.time >= nextAttackTime)
                    ComputeNextAttack();
                break;
            default:
                break;
        }
    }

    public void GoIdle()
    {
        state = BossState.Idle;
        nextAttackTime = Time.time + idleTime;
        Debug.Log("Starting idle");
    }

    private void ComputeNextAttack()
    {
        Debug.Log("Computing next attack");

        state = BossState.Attacking;

        //animator.SetTrigger("SmashAttack");
        //return;

        float r = Random.Range(0.0f, 1.0f);

        if (lastAttack == BossAttack.None)
        {
            if (r < 0.33f)
            {
                animator.SetTrigger("PuppetAttack");
                lastAttack = BossAttack.Puppet;
            }
            if (r < 0.66f)
            {
                animator.SetTrigger("ClapAttack");
                lastAttack = BossAttack.Clap;
            }
            else
            {
                animator.SetTrigger("SmashAttack");
                lastAttack = BossAttack.Smash;
            }
        }
        else if (lastAttack == BossAttack.Puppet)
        {
            if (r < 0.5f)
            {
                animator.SetTrigger("ClapAttack");
                lastAttack = BossAttack.Clap;
            }
            else
            {
                animator.SetTrigger("SmashAttack");
                lastAttack = BossAttack.Smash;
            }
        }
        else if (lastAttack == BossAttack.Clap)
        {
            if (r < 0.5f)
            {
                animator.SetTrigger("PuppetAttack");
                lastAttack = BossAttack.Puppet;
            }
            else
            {
                animator.SetTrigger("SmashAttack");
                lastAttack = BossAttack.Smash;
            }
        }
        else if (lastAttack == BossAttack.Smash)
        {
            if (r < 0.5f)
            {
                animator.SetTrigger("PuppetAttack");
                lastAttack = BossAttack.Puppet;
            }
            else
            {
                animator.SetTrigger("ClapAttack");
                lastAttack = BossAttack.Clap;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            health--;
            UpdateHealthbar();
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void UpdateHealthbar()
    {
        float lowerLimit = 0.094f;
        float upperLimit = 0.901f;
        float difference = upperLimit - lowerLimit;

        float healthPercent = (float)health / maxHealth;
        float fill = lowerLimit + healthPercent * difference;

        healthBarFill.fillAmount = fill;
    }
}
