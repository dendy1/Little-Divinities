using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    private MinionStats _stats;
    private MinionBaseStats _baseStats;
    private MinionInterface _interface;
    private StateMachine _stateMachine;

    public DieState(MinionStats stats, MinionBaseStats baseStats, MinionInterface @interface, StateMachine stateMachine)
    {
        _stats = stats;
        _baseStats = baseStats;
        _interface = @interface;
        _stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        _stats.dead?.Invoke();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
