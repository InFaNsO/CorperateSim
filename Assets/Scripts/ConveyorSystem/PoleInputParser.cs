using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleInputParser : MonoBehaviour
{
    Pole myPole = null;

    private void Awake()
    {
        myPole = GetComponentInParent<Pole>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        var resource = other.GetComponent<Resource>();
        if(resource && myPole.BeltOut)
        {
            resource.myCurrentBelt = myPole.BeltOut;
            resource.SetPath();
        }
        else if(resource)
        {
            var rb = resource.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
