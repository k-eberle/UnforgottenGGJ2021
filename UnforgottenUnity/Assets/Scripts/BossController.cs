using UnityEngine;

public class BossController : MonoBehaviour
{
    public float idleTime;
    public Animator animator;
    public int health;

    private enum BossState
    {
        Spawning,
        Idle,
        WaitingForAttack,
        Attacking
    }

    private BossState state;
    private float nextAttackTime;


    private void Start()
    {
        state = BossState.Spawning;
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

        animator.SetTrigger("SmashAttack");
        return;

        float r = Random.Range(0.0f, 1.0f);

        if (r < 0.5f)
            animator.SetTrigger("PuppetAttack");
        else
            animator.SetTrigger("ClapAttack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Boss trigger: " + collision.name);
    }
}
