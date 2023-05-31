
public class PlayerStateFactory 
{
    PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context=currentContext;
    }

    public BaseState Idle() 
    {
        return new PlayerIdleState(context, this);
    }
    public BaseState Walk() 
    {
        return new PlayerWalkState(context, this);
    }
    public BaseState Run() 
    {
        return new PlayerRunState(context, this);
    }
    public BaseState Airborne() 
    { 
        return new PlayerAirborneState(context, this);
    }
    public BaseState Grounded() 
    {
        return new PlayerGroundedState(context, this);
    }

    public BaseState Crouch()
    {
        return new PlayerCrouchState(context, this);
    }
    
    public BaseState Standing()
    {
        return new PlayerStandingState(context, this);
    }
}
