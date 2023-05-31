using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        context.CurrentMovementX = 0;
        context.CurrentMovementZ = 0;
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

    }
    public override void CheckSwitchState()
    {
        if (context.IsMovementPressed && !context.IsRunPressed)
        {
            SwitchState(factory.Walk());
        }
        else if (context.IsMovementPressed && context.IsRunPressed)
        {
            SwitchState(factory.Run());
        }
    }
}
