using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeState : IState
{
    private MinionStats _stats;
    private MinionBaseStats _baseStats;
    private MinionInterface _interface;
    private StateMachine _stateMachine;

    public MergeState(MinionStats stats, MinionBaseStats baseStats, MinionInterface @interface, StateMachine stateMachine)
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
    }

    public void Exit()
    {
    }
}
