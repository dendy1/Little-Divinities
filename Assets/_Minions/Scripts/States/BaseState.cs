public abstract class BaseState : IState
{
    protected MinionStats stats;

    protected BaseState(MinionStats stats)
    {
        this.stats = stats;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Execute()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}
