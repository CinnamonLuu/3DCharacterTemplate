using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    //animations
    int isWalkingHash;
    int isRunningHash;

    //movement
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 currentRunMovement;
    [SerializeField] private float runMultiplier = 3.0f;
    private bool isMovementPressed;
    private bool isRunPressed;
    [SerializeField] private float rotationFactorPerFrame = 15.0f;

    //gravity
    float groundedGravity = -0.05f;
    float gravity = -9.8f;

    //jump
    bool isJumpPressed = false;
    private float initialJumpVelocity;
    private float maxJumpHeight = 1.0f;
    private float maxJumpTime = 0.5f;
    private bool isJumping = false;

    BaseState currentState;
    PlayerStateFactory states;

    public CharacterController CharacterController => characterController;
    public Animator Animator => animator;
    public BaseState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
    public bool IsJumpPressed => isJumpPressed;
    public bool IsJumping
    {
        get { return isJumping; }
        set { isJumping = value; }
    }
    public float CurrentMovementX
    {
        get { return currentMovement.x; }
        set { currentMovement.x = value; }
    }
    public float CurrentMovementY
    {
        get { return currentMovement.y; }
        set { currentMovement.y = value; }
    }
    public float CurrentMovementZ
    {
        get { return currentMovement.z; }
        set { currentMovement.z = value; }
    }
    public Vector2 CurrentMovementInput => currentMovementInput;

    public float InitialJumpVelocity=> initialJumpVelocity;
    public float GroundedGravity => groundedGravity;
    public float Gravity => gravity;

    public bool IsMovementPressed=> isMovementPressed;
    public bool IsRunPressed => isRunPressed;
    public int IsWalkingHash => isWalkingHash;
    public int IsRunningHash => isRunningHash;

    public float RunMultiplier=> runMultiplier;

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }


    private void Awake()
    {
        playerInput = new PlayerInput();

        //setup state
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += onMovmentInput;
        playerInput.CharacterControls.Move.canceled += onMovmentInput;
        playerInput.CharacterControls.Move.performed += onMovmentInput;

        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;

        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;
        SetUpJumpVariables();
    }

    private void SetUpJumpVariables()
    {
        float timeToApex = maxJumpTime / 2f;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void Update()
    {
        HandleRotation();
        currentState.UpdateStates();
        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovementInput.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = currentMovementInput.y;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void onMovmentInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }}
