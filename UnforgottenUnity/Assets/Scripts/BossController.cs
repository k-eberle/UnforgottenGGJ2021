using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
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
                StartCoroutine(ComputeNextAttack());
                break;
            default:
                break;
        }
    }

    private IEnumerator ComputeNextAttack()
    {
        state = BossState.WaitingForAttack;
        Debug.Log("Waiting.");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Waited.");
        state = BossState.Idle;
    }

    public void GoIdle()
    {
        state = BossState.Idle;
    }
}
