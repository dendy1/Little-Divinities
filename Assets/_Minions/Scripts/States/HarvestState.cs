using UnityEngine;

public class HarvestState : BaseState
{
    private Delay _idleDelay = new Delay(1f);
    private Delay _harvestDelay = new Delay(1f);

    public HarvestState(MinionStats stats) : base(stats) { }

    public override void Enter()
    {
        _idleDelay.WaitTime = Random.Range(0f, 5f);
        
        _harvestDelay.Reset();
        _idleDelay.Reset();
    }

    public override void Execute()
    {
        if (_harvestDelay.IsReady)
        {
            GameManager.Instance.Harvest(stats.BaseStats.resourceTypeProduction, stats.currentHarvestRate);
            stats.currentHp -= stats.currentFatigueRate;
            stats.Interface.UpdateHealthBar();

            _harvestDelay.Reset();
            if (stats.currentHp <= 0)
            {
                stats.StateMachine.ChangeState(stats.dieState);
            }
        }

        if (_idleDelay.IsReady)
        {
            stats.StateMachine.ChangeState(stats.idleState);
            _idleDelay.Reset();
        }
    }
}
