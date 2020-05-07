using UnityEngine;

public class HarvestState : IState
{
    private MinionStats _stats;
    private MinionBaseStats _baseStats;
    private MinionInterface _interface;
    private StateMachine _stateMachine;

    private Delay _idleDelay = new Delay(4f);
    private Delay _harvestDelay = new Delay(1f);
    
    public HarvestState(MinionStats stats, MinionBaseStats baseStats, MinionInterface @interface, StateMachine stateMachine)
    {
        _stats = stats;
        _baseStats = baseStats;
        _interface = @interface;
        _stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        _harvestDelay.Reset();
        _idleDelay.Reset();
    }

    public void Execute()
    {
        if (_harvestDelay.IsReady)
        {
            GameManager.Instance.Harvest(_baseStats.resourceTypeProduction, _stats.currentHarvestRate);
            _stats.currentHp -= _stats.currentFatigueRate;
            _interface.UpdateHealthBar();

            _harvestDelay.Reset();
            if (_stats.currentHp <= 0)
            {
                _stateMachine.ChangeState(_stats.dieState);
            }
        }

        if (_idleDelay.IsReady)
        {
            _stateMachine.ChangeState(new IdleState(_stats, _baseStats, _interface, _stateMachine));
            _idleDelay.WaitTime = Random.Range(5.0f, 10);

            _idleDelay.Reset();
        }
    }

    public void Exit()
    {
    }
}
