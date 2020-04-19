using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneColliderParcer : MonoBehaviour
{
    Drone myDrone;

    Collider ResourseCollider;
    Collider FactoryCollider;
    Collider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myDrone = GetComponentInParent<Drone>();
        myCollider = myDrone.GetComponent<Collider>();
        ResourseCollider = myDrone.CollectionPoint.gameObject.GetComponent<Collider>();
        FactoryCollider = myDrone.DropPoint.gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var resourceNode = other.GetComponentInParent<ResourceNode>();
        if (resourceNode)
        {
            myDrone.myCurrentNode = resourceNode;
            return;
        }
    }
}
