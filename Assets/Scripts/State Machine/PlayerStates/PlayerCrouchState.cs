using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : BaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory currentFactory) : base(currentContext, currentFactory) 
    {
        InitializeSubState();
    }

    public override void EnterState()
    {
        context.Animator.SetBool(context.IsCrouchingHash, true);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {
        context.Animator.SetBool(context.IsCrouchingHash, false);
    }

    public override void InitializeSubState()
    {
        if (!context.IsMovementPressed && !context.IsRunPressed)
        {
            SetSubState(factory.Idle());
        }
        else if (context.IsMovementPressed && !context.IsRunPressed)
        {
            SetSubState(factory.Walk());
        }
    }

    public override void CheckSwitchState()
    {
        if (!context.IsCrouchPressed)
        {
            SwitchState(factory.Standing());
        }
    }
}
