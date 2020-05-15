using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] float TurnSpeed = 5.0f;
    [SerializeField] float MoveSpeed = 10.0f;
    [SerializeField] float RunBost = 1.5f;

    Camera myCamera = null;

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

    Transform startBeltAt;
    Transform endBeltAt;

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
        float moveForward = movementInput.y * MoveSpeed * Time.deltaTime;

        //for jump
        if (IsJumping && Time.time > nextJumpTime)
        {
            nextJumpTime = Time.time + JumpDuration;
            myAnimator.SetTrigger(jumpTrig);
        }
        else if (Mathf.Abs(movementInput.y) < 0.1f && !IsCrouching)
        {
            if (!IsJumping && Time.time > nextJumpTime && currentAnim != 0 && !IsCrouching)
            {
                currentAnim = 0;
                myAnimator.SetInteger(mainEnum, currentAnim);
            }
        }
        else if (movementInput.y < 0.0f)
        {
            currentAnim = 3;
            myAnimator.SetInteger(mainEnum, currentAnim);
        }
        else if(IsRunning && IsCrouching)
        {
            if(currentAnim == 2 && !myAnimator.GetBool(sliding))
            {
                myAnimator.SetTrigger(slideTrig);
            }
            myAnimator.SetBool(sliding, IsCrouching);
            moveForward *= RunBost * 2.0f;
        }
        else
        {

            if (IsRunning && !IsCrouching)
            {
                moveForward *= RunBost;
                if(myAnimator.GetBool(sliding))
                {
                    myAnimator.SetBool(sliding, IsCrouching);
                }
                else if (currentAnim != 2)
                {
                    currentAnim = 2;
                    myAnimator.SetInteger(mainEnum, currentAnim);
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
            if (movementInput.x != 0.0f)
            {
                //transform.Rotate(Vector3.up, movementInput.x * TurnSpeed);
            }
        }
        if(!IsCrouching && myAnimator.GetBool(sliding))
        {
            myAnimator.SetBool(sliding, false);
        }

        Vector3 delta = new Vector3(cameraTransform.forward.x, 0.0f, cameraTransform.forward.z);
        transform.LookAt(transform.position + delta.normalized);
        transform.position += transform.forward * moveForward;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
        Gizmos.DrawWireSphere(transform.position + transform.forward * 5.0f, 0.20f);
    }
}
