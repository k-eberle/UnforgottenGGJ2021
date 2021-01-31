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
        animator.SetBool("Under66", false);
        animator.SetBool("Under33", false);
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
    }

    private void ComputeNextAttack()
    {
        state = BossState.Attacking;

        //animator.SetTrigger("SmashAttack");
        //return;

        float r = Random.Range(0.0f, 1.0f);

        if (lastAttack == BossAttack.None)
        {
            if (r < 0.33f)
            {
                DoPuppetAttack();
            }
            else if (r < 0.66f)
            {
                DoClapAttack();
            }
            else
            {
                DoSmashAttack();
            }
        }
        else if (lastAttack == BossAttack.Puppet)
        {
            if (r < 0.5f)
            {
                DoClapAttack();
            }
            else
            {
                DoSmashAttack();
            }
        }
        else if (lastAttack == BossAttack.Clap)
        {
            if (r < 0.5f)
            {
                DoPuppetAttack();
            }
            else
            {
                DoSmashAttack();
            }
        }
        else if (lastAttack == BossAttack.Smash)
        {
            if (r < 0.5f)
            {
                DoPuppetAttack();
            }
            else
            {
                DoClapAttack();
            }
        }

    }

    private void DoPuppetAttack()
    {
        animator.SetTrigger("PuppetAttack");
        lastAttack = BossAttack.Puppet;
    }

    private void DoClapAttack()
    {
        animator.SetTrigger("ClapAttack");
        lastAttack = BossAttack.Clap;
    }

    private void DoSmashAttack()
    {
        animator.SetTrigger("SmashAttack");
        lastAttack = BossAttack.Smash;
    }

    public void PlayPuppetSound()
    {
        Debug.Log("PuppetSound");
        AudioManager.PlaySound("BossPuppet");
    }

    public void PlayClapSound()
    {
        AudioManager.PlaySound("BossClap");
    }

    public void PlaySmashSound()
    {
        AudioManager.PlaySound("BossSmash");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            health--;
            UpdateHealthbar();

            animator.SetTrigger("DazeBoss");
            animator.SetBool("Under66", 100 * health < 66 * maxHealth);
            animator.SetBool("Under33", 100 * health < 33 * maxHealth);
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

    public void AfterSpawnDialogue()
    {
        GameObject.FindObjectOfType<LevelManager>().ShowCustomText("No... That cannot be... You look like...");
    }

    public void OnDestroy()
    {
        GameObject.FindObjectOfType<LevelManager>().ShowCustomText("Was that me all along?\nHorrible, but... also comforting.\nI can handle that. I can handle myself.", 3f);
        GameObject.FindObjectOfType<LevelManager>().ShowCustomTextNext("For the fist time in what seemed like an eternity -\nI open my eyes");
    }
}
