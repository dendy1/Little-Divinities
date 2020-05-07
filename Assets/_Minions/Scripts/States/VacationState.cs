public class VacationState : IState
{
    private MinionStats _stats;
    private MinionBaseStats _baseStats;
    private MinionInterface _interface;
    private StateMachine _stateMachine;

    private Delay _healDelay = new Delay(3f);
    
    public VacationState(MinionStats stats, MinionBaseStats baseStats, MinionInterface @interface, StateMachine stateMachine)
    {
        _stats = stats;
        _baseStats = baseStats;
        _interface = @interface;
        _stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        _stats.StopAllCoroutines();
        _healDelay.Reset();
    }

    public void Execute()
    {
        if (_healDelay.IsReady)
        {
            if (_stats.currentHp + _stats.currentFatigueRate > _baseStats.hp)
            {
                _stats.currentHp = _baseStats.hp;
            }
            else
            {
                _stats.currentHp += _stats.currentFatigueRate;
            }

            _interface.UpdateHealthBar();
            _interface.SetActiveGoodHPImage(true);
            
            _healDelay.Reset();
        }
    }

    public void Exit()
    {
        _interface.SetActiveGoodHPImage(false);
    }
}
