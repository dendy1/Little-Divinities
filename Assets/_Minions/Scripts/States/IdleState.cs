using System.Collections;
using UnityEngine;

public class IdleState : IState
{    
    private MinionStats _stats;
    private MinionBaseStats _baseStats;
    private MinionInterface _interface;
    private StateMachine _stateMachine;

    public IdleState(MinionStats stats, MinionBaseStats baseStats, MinionInterface @interface, StateMachine stateMachine)
    {
        _stats = stats;
        _baseStats = baseStats;
        _interface = @interface;
        _stateMachine = stateMachine;
    }
    
    public void Enter()
    {
    }

    public void Execute()
    {
        _stats.StartCoroutine(BackToHarvest());
    }

    public void Exit()
    {
    }
    
    private IEnumerator BackToHarvest()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        _stateMachine.ChangeState(_stats.harvestState);
    }
}
