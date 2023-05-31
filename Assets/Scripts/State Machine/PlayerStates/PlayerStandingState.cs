using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandingState : BaseState
{
    public PlayerStandingState(PlayerStateMachine currentContext, PlayerStateFactory currentFactory) : base(currentContext, currentFactory) 
    {
        InitializeSubState();
    }

    public override void EnterState()
    {
        context.Animator.SetBool(context.IsCrouchingHash, false);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
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
        else if (context.IsMovementPressed && context.IsRunPressed)
        {
            SetSubState(factory.Run());
        }
    }

    public override void CheckSwitchState()
    {
        if (context.IsCrouchPressed)
        {
            SwitchState(factory.Crouch());
        }
    }
}
