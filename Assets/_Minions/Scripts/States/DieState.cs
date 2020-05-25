public class DieState : BaseState
{
    public DieState(MinionStats stats) : base(stats) {}
    
    public override void Enter()
    {
        stats.dead?.Invoke();
    }
}
