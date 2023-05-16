

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
        if(!context.IsMovementPressed && !context.IsRunPressed)
        {
            SetSubState(factory.Idle());
        }else if(context.IsMovementPressed && !context.IsRunPressed)
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
        if(context.IsJumpPressed)
        {
            SwitchState(factory.Jump());
        }
    }
}
