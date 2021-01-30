using UnityEngine;

public class BossController : MonoBehaviour
{
    private enum BossState
    {
        Start,
        Spawning,
        Idle
    }

    private BossState state;


    private void Start()
    {
        state = BossState.Start;
    }
    
    private void Update()
    {
        switch (state)
        {
            case BossState.Start:
                AnimateSpawn();
                break;
            default:
                break;
        }
    }

    private void AnimateSpawn()
    {

    }
}
