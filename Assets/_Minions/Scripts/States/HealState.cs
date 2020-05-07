public class HealState : IState
{
    private MinionStats _stats;
    private MinionBaseStats _baseStats;
    private MinionInterface _interface;
    private StateMachine _stateMachine;

    private Delay _healDelay = new Delay(1f);
    
    public HealState(MinionStats stats, MinionBaseStats baseStats, MinionInterface @interface, StateMachine stateMachine)
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
        _stats.currentHp += _stats.currentFatigueRate;
        _interface.UpdateHealthBar();

        if (_healDelay.IsReady)
        {
            _interface.SetActiveGoodHPImage(true);
            
            _healDelay.Reset();
        }
    }

    public void Exit()
    {
        _interface.SetActiveGoodHPImage(false);
    }
}
