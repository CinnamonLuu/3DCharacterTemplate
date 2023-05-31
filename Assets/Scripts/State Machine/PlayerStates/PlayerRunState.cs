using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : BaseState
{

    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }


    public override void EnterState()
    {
        context.Animator.SetBool(context.IsWalkingHash, true);
        context.Animator.SetBool(context.IsRunningHash, true);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        context.CurrentMovementX = context.CurrentMovementInput.x * context.RunMultiplier;
        context.CurrentMovementZ = context.CurrentMovementInput.y * context.RunMultiplier;
    }

    public override void ExitState()
    {
        context.Animator.SetBool(context.IsWalkingHash, false);
        context.Animator.SetBool(context.IsRunningHash, false);
    }

    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchState()
    {
        if (context.IsMovementPressed && !context.IsRunPressed)
        {
            SwitchState(factory.Walk());
        }
        else if (!context.IsMovementPressed)
        {
            SwitchState(factory.Idle());
        }
    }
}
