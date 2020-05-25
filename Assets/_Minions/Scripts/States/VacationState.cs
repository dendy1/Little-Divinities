public class VacationState : BaseState
{
    private Delay _healDelay = new Delay(3f);
    
    public VacationState(MinionStats stats) : base(stats) {}
    
    public override void Enter()
    {
        stats.StopAllCoroutines();
        _healDelay.Reset();
    }

    public override void Execute()
    {
        if (_healDelay.IsReady)
        {
            if (stats.currentHp + stats.currentFatigueRate > stats.BaseStats.hp)
            {
                stats.currentHp = stats.BaseStats.hp;
            }
            else
            {
                stats.currentHp += stats.currentFatigueRate;
            }

            stats.Interface.UpdateHealthBar();
            stats.Interface.SetActiveGoodHPImage(true);
            
            _healDelay.Reset();
        }
    }

    public override void Exit()
    {
        stats.Interface.SetActiveGoodHPImage(false);
    }
}
