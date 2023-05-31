using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : BaseState
{
    public PlayerAirborneState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        context.Animator.SetBool(context.IsJumpingHash, true);
        HandleJump();
    }

    public override void ExitState()
    {
        context.Animator.SetBool(context.IsJumpingHash, false);
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

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (context.CharacterController.isGrounded)
        {
            SwitchState(factory.Grounded());
        }
    }

    private void HandleJump()
    {
        context.IsJumping = true;
        context.CurrentMovementY = context.InitialJumpVelocity;
    }

    private void HandleGravity()
    {
        context.CurrentMovementY += context.Gravity * Time.deltaTime;
    }
}
