using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDroneControll : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook myCam = null;
    Transform cameraTransform = null;
    [SerializeField] float Dampening  = 0.5f;
    [SerializeField] float HeightSpeed  = 5.0f;
    [SerializeField] float MoveSpeed = 10.0f;

    [SerializeField] RectTransform UI;

    DroneActions droneInputs;
    Rigidbody myRb;

    Vector2 moveInput = new Vector2();
    bool isMoving = false;

    float altitude = 0.0f;
    bool isUpDown = false;

    private void Awake()
    {
        cameraTransform = myCam.transform;

        droneInputs = new DroneActions();
        droneInputs.DroneMap.Move.started += ctx => isMoving = true;
        droneInputs.DroneMap.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        droneInputs.DroneMap.Move.canceled += ctx => isMoving = false;
        
        droneInputs.DroneMap.Altitude.started += ctx => isUpDown = true;
        droneInputs.DroneMap.Altitude.performed += ctx => altitude = ctx.ReadValue<float>();
        droneInputs.DroneMap.Altitude.canceled += ctx => isUpDown = false;

        droneInputs.Enable();

        myRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(moveInput.x != 0.0f || moveInput.y != 0.0f)
        {
            if(!isMoving)
            {
                moveInput.x = moveInput.x < 0 ? Mathf.Min(0.0f, moveInput.x + Dampening) : Mathf.Max(0.0f, moveInput.x - Dampening);
                moveInput.y = moveInput.y < 0 ? Mathf.Min(0.0f, moveInput.y + Dampening) : Mathf.Max(0.0f, moveInput.y - Dampening);
            }
        }
        if(!isUpDown && altitude != 0f)
        {
            altitude = altitude < 0f ? Mathf.Min(0.0f, altitude + Dampening) : Mathf.Max(0.0f, altitude - Dampening);
        }


        if(UI.gameObject.activeSelf)
        {
            myCam.enabled = false;
            return;
        }
        else if(!UI.gameObject.activeSelf && !myCam.enabled)
        {
            myCam.enabled = true;
        }

        //turn
        Vector3 delta = new Vector3(cameraTransform.forward.x, 0.0f, cameraTransform.forward.z);
        transform.LookAt(transform.position + delta.normalized);

        float dt =  Time.deltaTime;

        //move
        Vector3 move = new Vector3();
        move = (transform.forward * moveInput.y * MoveSpeed * dt) + 
            (transform.right * moveInput.x * MoveSpeed * dt) + 
            (transform.up * altitude * HeightSpeed * dt);

        //myRb.AddRelativeForce(move);

        transform.position += move;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}

