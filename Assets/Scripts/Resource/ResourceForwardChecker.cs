using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceForwardChecker : MonoBehaviour
{
    Rigidbody myRB = null;
    Resource myResource = null;

    private void Awake()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myResource = GetComponentInParent<Resource>();
    }


    private void OnTriggerStay(Collider other)
    {
        myRB.velocity = Vector3.zero;
        myResource.stopPath = true;
    }

    private void OnTriggerExit(Collider other)
    {
        myResource.stopPath = false;
    }

}
