public class StateMachine
{
    private IState _currentState;

    public IState CurrentState => _currentState;

    public void Update()
    {
        _currentState?.Execute();
    }

    public void ChangeState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}
