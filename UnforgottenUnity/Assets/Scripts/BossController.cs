using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float idleTime;
    public Animator animator;

    private enum BossState
    {
        Spawning,
        Idle,
        WaitingForAttack,
        PuppetAttack,
        SmashAttack,
        ClapAttack
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

        state = BossState.PuppetAttack;
        animator.SetTrigger("PuppetAttack");
    }

    public void HitPuppet()
    {
        Debug.Log("Hit puppet!");
    }
}
