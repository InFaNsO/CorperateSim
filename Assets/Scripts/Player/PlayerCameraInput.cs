using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraInput : MonoBehaviour
{
    // Start is called before the first frame update
    Cinemachine.CinemachineFreeLook cam = null;
    PlayerActions playerInput = null;

    [SerializeField] float LookSpeed = 2.0f;
    Vector2 camMovement = new Vector2();

    private void Awake()
    {
        cam = GetComponent<Cinemachine.CinemachineFreeLook>();
        playerInput = new PlayerActions();
        playerInput.MoveAction.Look.performed += ctx => camMovement = ctx.ReadValue<Vector2>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cam.m_YAxis.m_InputAxisValue = camMovement.y * LookSpeed;
        cam.m_XAxis.m_InputAxisValue = camMovement.x * LookSpeed;
    }
}
