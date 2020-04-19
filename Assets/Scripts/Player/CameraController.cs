using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    Vector3 cameraOffset = new Vector3();

    [Range(0.01f, 1.0f)] float Smoothness = 0.3f;

    [SerializeField] bool Inverted = false;
    [SerializeField] float lookSpeed = 5.0f;
    [SerializeField] float moveThreshHold = 0.1f;
    PlayerActions cameraActions;
    Vector2 lookVal = new Vector2();
    private void Awake()
    {
        cameraActions = new PlayerActions();
        cameraActions.MoveAction.Look.performed += ctx => lookVal = ctx.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(lookVal.sqrMagnitude > moveThreshHold)
        {
            var turnAngle = Quaternion.AngleAxis(lookVal.x * lookSpeed, transform.up);
            cameraOffset = turnAngle * cameraOffset;
            turnAngle = Quaternion.AngleAxis(lookVal.y * lookSpeed, transform.right);
            cameraOffset = turnAngle * cameraOffset;
        }

        transform.position = Vector3.Slerp(transform.position, playerTransform.position + cameraOffset, Smoothness);

        transform.LookAt(playerTransform);
    }

    private void OnEnable()
    {
        cameraActions.Enable();
    }
    private void OnDisable()
    {
        cameraActions.Disable();
    }
}
