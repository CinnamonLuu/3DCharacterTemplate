using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : BaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        context.Animator.SetBool(context.IsWalkingHash, true);
        context.Animator.SetBool(context.IsRunningHash, false);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        context.CurrentMovementX = context.CurrentMovementInput.x;
        context.CurrentMovementZ = context.CurrentMovementInput.y;
    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchState()
    {
        if (!context.IsMovementPressed)
        {
            SwitchState(factory.Idle());
        }
        else if (context.IsMovementPressed && context.IsRunPressed)
        {
            SwitchState(factory.Run());
        }

    }
}
