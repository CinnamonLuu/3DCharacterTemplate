using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : BaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        context.Animator.SetBool(context.IsWalkingHash, true);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        context.CurrentMovementX = context.CurrentMovementInput.x * context.WalkMultiplier;
        context.CurrentMovementZ = context.CurrentMovementInput.y * context.WalkMultiplier;
    }

    public override void ExitState()
    {
        context.Animator.SetBool(context.IsWalkingHash, false);
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
