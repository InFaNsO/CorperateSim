using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum FactoryColliderLocation
{
    Entry,
    Exit
}

public class FactoryColliderParcer : MonoBehaviour
{
    Factory myFactory;
    [SerializeField] public FactoryColliderLocation myLocation = FactoryColliderLocation.Entry;
    

    // Start is called before the first frame update
    void Start()
    {
        myFactory = GetComponentInParent<Factory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var dr = other.GetComponent<Drone>();
        if(dr)
        {
            if(myLocation == FactoryColliderLocation.Entry)
            {
                dr.ResourcesGiven = myFactory.TakeResourceFromDrone(dr);
            }
            else if(myLocation == FactoryColliderLocation.Exit)
            {
                dr.ResourcesTaken = myFactory.GiveResourceToDrone(dr);
            }
            return;
        }
    }
}
