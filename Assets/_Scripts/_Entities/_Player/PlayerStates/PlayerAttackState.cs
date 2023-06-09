using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine ctx, PlayerStateFactory factory, PlayerState state) : base(ctx, factory, state)
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

    protected override void CheckSwitchState()
    {
        if (Ctx.Controller.IsMoving)
        {
            Ctx.SwitchState(Factory.Moving);
        }
        else
        {
            Ctx.SwitchState(Factory.Idle);
        }
    }
}