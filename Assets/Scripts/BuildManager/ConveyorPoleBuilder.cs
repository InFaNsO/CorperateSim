using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ConveyorPoleBuilder : BuilderBase
{
    [SerializeField] float rotationSpeed = 2.0f;

    int GroundMask = 0;

    float playerRange = float.MaxValue;

    [HideInInspector] public GameObject currentPole = null;

    private void Awake()
    {
        GroundMask = LayerMask.GetMask("Ground"); //add structures mask aswell
    }

    public override bool ParseInput(ref Ray mouseRay)
    {
        if(currentPole)
        {
            currentPole = null;
            return true;
        }
        return false;
    }

    public override void CustomReset()
    {
        if (currentPole)
            Destroy(currentPole);
    }

    public override void UpdateObject()
    {
        if (!currentPole)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, playerRange, GroundMask))
            {
                currentPole = Instantiate(buildObject, hit.point, Quaternion.identity);
            }
        }
    
        var d = Input.GetAxis("MouseScroll");
        if (d != 0f)
            currentPole.transform.Rotate(currentPole.transform.up, rotationSpeed * d);

        Ray mouseR = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;
        if (Physics.Raycast(mouseR, out h, playerRange, GroundMask))
        {
            currentPole.transform.position = h.point;
        }
    }
}
