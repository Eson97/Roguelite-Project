using System.Collections.Generic;

public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    private Dictionary<PlayerState, PlayerBaseState> _states;

    public PlayerStateFactory(PlayerStateMachine context)
    {
        _context = context;
        _states = new();

        _states[PlayerState.Idle] = new PlayerIdleState(_context, this, PlayerState.Idle);
        _states[PlayerState.Walking] = new PlayerWalkingState(_context, this, PlayerState.Walking);
        _states[PlayerState.Attack] = new PlayerAttackState(_context, this, PlayerState.Attack);
    }

    public PlayerBaseState Idle => _states[PlayerState.Idle];
    public PlayerBaseState Moving => _states[PlayerState.Walking];
    public PlayerBaseState Attack => _states[PlayerState.Attack];

}

public enum PlayerState
{
    Idle,
    Walking,
    Attack,
}