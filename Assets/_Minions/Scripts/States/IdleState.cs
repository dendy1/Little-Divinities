using System.Collections;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(MinionStats stats) : base(stats) {}
    
    public override void Execute()
    {
        stats.StartCoroutine(BackToHarvest());
    }
    
    private IEnumerator BackToHarvest()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        stats.StateMachine.ChangeState(stats.harvestState);
    }
}
