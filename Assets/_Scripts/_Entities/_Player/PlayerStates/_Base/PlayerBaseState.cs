
public abstract class PlayerBaseState : IState
{
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerState _state;

    protected PlayerStateMachine Ctx => _ctx;
    protected PlayerStateFactory Factory => _factory;

    public PlayerState State => _state;

    public PlayerBaseState(PlayerStateMachine ctx, PlayerStateFactory factory, PlayerState state)
    {
        _ctx = ctx;
        _factory = factory;
        _state = state;
    }

    public abstract void EnterState();
    public abstract void ExitState();

    public virtual void UpdateState() => CheckSwitchState();
    public virtual void FixedUpdateState() { }

    protected abstract void CheckSwitchState();
}
