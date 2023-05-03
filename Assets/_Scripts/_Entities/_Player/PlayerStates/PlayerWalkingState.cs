using UnityEngine;

public class PlayerWalkingState : PlayerBaseState
{
    public PlayerWalkingState(PlayerStateMachine ctx, PlayerStateFactory factory, PlayerState state) : base(ctx, factory, state)
    {
    }

    public override void EnterState()
    {
        Ctx.Log($"<color=teal>Enter</color> {State} state");
    }

    public override void ExitState()
    {
        Ctx.Log($"<color=red>Exit</color> {State} state");
    }

    public override void FixedUpdateState()
    {
        Ctx.Rigidbody.velocity = Time.fixedDeltaTime * Ctx.Speed * Ctx.Controller.MoveDirection;
    }

    protected override void CheckSwitchState()
    {
        if (Ctx.Controller.IsAttackPressed)
        {
            Ctx.SwitchState(Factory.Attack);
        }
        else if (!Ctx.Controller.IsMoving)
        {
            Ctx.SwitchState(Factory.Idle);
        }
    }
}
