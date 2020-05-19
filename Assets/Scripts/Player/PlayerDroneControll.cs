using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDroneControll : MonoBehaviour
{
    public float CameraSensitivity = 90;
    public float climbSpeed = 4;
    public float normalMoveSpeed = 10;
    public float moveFactor = 0.25f;

    float rotationX = 0f;
    float rotationY = 0f;

    DroneActions droneInputs;
    Vector2 moveInput = new Vector2();
    bool isMoving = false;

    float altitude = 0.0f;
    bool isUpDown = false;

    float speedBuf = 0.0f;
    bool isSpeedBuf = false;













    [SerializeField] CinemachineFreeLook myCam = null;
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] float Dampening  = 0.5f;
    [SerializeField] float HeightSpeed  = 5.0f;
    [SerializeField] float MoveSpeed = 10.0f;

    [SerializeField] RectTransform UI;

    Rigidbody myRb;

    private void Awake()
    {
        //cameraTransform = myCam.transform;

        droneInputs = new DroneActions();
        droneInputs.DroneMap.Move.started += ctx => isMoving = true;
        droneInputs.DroneMap.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        droneInputs.DroneMap.Move.canceled += ctx => isMoving = false;
        
        droneInputs.DroneMap.Altitude.started += ctx => isUpDown = true;
        droneInputs.DroneMap.Altitude.performed += ctx => altitude = ctx.ReadValue<float>();
        droneInputs.DroneMap.Altitude.canceled += ctx => isUpDown = false;

        droneInputs.DroneMap.Speed.started += ctx => isSpeedBuf = true;
        droneInputs.DroneMap.Speed.performed += ctx => speedBuf = ctx.ReadValue<float>();
        droneInputs.DroneMap.Speed.canceled += ctx => isSpeedBuf = true;

        droneInputs.Enable();

        myRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    Vector3 prevForward;

    void Update()
    {
        //turn
        Vector3 delta = new Vector3(cameraTransform.forward.x, 0.0f, cameraTransform.forward.z);
        transform.LookAt(transform.position + delta.normalized);

        DoDampening();
        if(isSpeedBuf && isMoving)
        {
            transform.position += transform.forward * (normalMoveSpeed * moveFactor) * moveInput.y * Time.deltaTime;
            transform.position += transform.right * (normalMoveSpeed * moveFactor) * moveInput.x * Time.deltaTime;
        }
        else if(isMoving)
        {
            transform.position += transform.forward * normalMoveSpeed * moveInput.y * Time.deltaTime;
            var r = transform.right * normalMoveSpeed * moveInput.x * Time.deltaTime; ;

            transform.position += r;
            cameraTransform.position += r;
        }
        if(isUpDown)
        {
            transform.position += transform.up * climbSpeed * altitude * Time.deltaTime;
        }


        if(UI.gameObject.activeSelf)
        {
            myCam.enabled = false;
            droneInputs.Disable();
            return;
        }
        else if(!UI.gameObject.activeSelf && !myCam.enabled)
        {
            myCam.enabled = true;
            droneInputs.Enable();
        }



        //float dt =  Time.deltaTime;
        //
        ////move
        //Vector3 move = new Vector3();
        //move = (transform.forward * moveInput.y * MoveSpeed * dt) + 
        //    (transform.right * moveInput.x * MoveSpeed * dt) + 
        //    (transform.up * altitude * HeightSpeed * dt);
        //
        ////myRb.AddRelativeForce(move);
        //
        //transform.position += move;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    void SetRotation()
    {
        rotationX += Input.GetAxis("MouseX") + CameraSensitivity + Time.deltaTime;
        rotationY += Input.GetAxis("MouseY") + CameraSensitivity + Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
    }
    void DoDampening()
    {
        if (moveInput.x != 0.0f || moveInput.y != 0.0f)
        {
            if (!isMoving)
            {
                moveInput.x = moveInput.x < 0 ? Mathf.Min(0.0f, moveInput.x + Dampening) : Mathf.Max(0.0f, moveInput.x - Dampening);
                moveInput.y = moveInput.y < 0 ? Mathf.Min(0.0f, moveInput.y + Dampening) : Mathf.Max(0.0f, moveInput.y - Dampening);
            }
        }
        if (!isUpDown && altitude != 0f)
        {
            altitude = altitude < 0f ? Mathf.Min(0.0f, altitude + Dampening) : Mathf.Max(0.0f, altitude - Dampening);
        }
    }
}

