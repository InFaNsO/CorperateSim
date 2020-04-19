using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 10.0f;
    [SerializeField] float RunBost = 1.5f;

    Animator myAnimator = null;
    string mainEnum = "MainEnum";
    string slideTrig = "SlideTrig";
    string sliding = "Sliding";
    string jumpTrig = "JumpTrig";

    PlayerActions playerInput;
    Vector2 movementInput = new Vector2();
    bool IsRunning = false;
    bool IsCrouching = false;
    bool IsJumping = false;

    const float JumpDuration = 0.83f;   //change it if the animation changes
    float nextJumpTime = 0.0f;
    int currentAnim = 0;

    private void Awake()
    {
        playerInput = new PlayerActions();
        playerInput.MoveAction.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        playerInput.MoveAction.Run.started += ctx => IsRunning = true;
        playerInput.MoveAction.Run.canceled += ctx => IsRunning = false;
        playerInput.MoveAction.Crouch.started += ctx => IsCrouching = true;
        playerInput.MoveAction.Crouch.canceled += ctx => IsCrouching = false;
        playerInput.MoveAction.Jump.started += ctx => IsJumping = true;
        playerInput.MoveAction.Jump.canceled += ctx => IsJumping = false;
    }

    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        //for jump
        if (IsJumping && Time.time > nextJumpTime)
        {
            nextJumpTime = Time.time + JumpDuration;
            myAnimator.SetTrigger(jumpTrig);
        }
        else if (movementInput.sqrMagnitude < 0.1f)
        {
            if (!IsJumping && Time.time > nextJumpTime && currentAnim != 0 && !IsCrouching)
            {
                currentAnim = 0;
                myAnimator.SetInteger(mainEnum, currentAnim);
            }
        }
        else
        {
            Vector3 move = new Vector3(movementInput.x, 0.0f, movementInput.y) * MoveSpeed * Time.deltaTime;

            if (IsRunning)
            {
                if (IsCrouching)
                {
                    myAnimator.SetTrigger(slideTrig);
                    myAnimator.SetBool(sliding, IsCrouching);
                    move *= RunBost * 2.0f;
                }
                else
                {
                    move *= RunBost;
                    myAnimator.SetBool(sliding, IsCrouching);
                    if (currentAnim != 2)
                    {
                        currentAnim = 2;
                        myAnimator.SetInteger(mainEnum, currentAnim);
                    }
                }
            }
            else
            {
                if (currentAnim != 1)
                {
                    currentAnim = 1;
                    myAnimator.SetInteger(mainEnum, currentAnim);
                }
            }
            transform.Translate(move);
        }
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
}
