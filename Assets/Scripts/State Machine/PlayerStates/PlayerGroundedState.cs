

using UnityEngine;

public class PlayerGroundedState : BaseState
{

    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        context.CurrentMovementY = context.GroundedGravity;
    }

    public override void UpdateState()
    {
        CheckSwitchState();

    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {
        if (context.IsCrouchPressed)
        {
            SetSubState(factory.Crouch());
        }
        else if(!context.IsCrouchPressed)
        {
            SetSubState(factory.Standing());
        }
    }

    public override void CheckSwitchState()
    {
        if (context.IsJumpPressed )//|| !context.CharacterController.isGrounded)
        {
            SwitchState(factory.Airborne());
        }
    }
}
