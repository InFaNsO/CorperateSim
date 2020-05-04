using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneUnloadingState : State
{
    Drone drone;


    // Start is called before the first frame update
    void Start()
    {
        drone = GetComponentInParent<Drone>();
    }

    public override void Enter()
    {
        Debug.Log("Entered Unloading State");
        drone.myAgent.SetDestination(drone.transform.position);
    }

    public override void MyUpdate()
    {
        /*if (drone.mCurrentCapacity < 1.0f || drone.myCurrentFactoryDropOff.TakeResourceFromDrone(drone))
        {
            drone.myStatemachine.ChangeState(DroneStates.GoingToCollect.ToString());
        }*/
    }

    public override void Exit()
    {
        Debug.Log("Entered Unloading State");
        drone.myCarryingResource = null;
        Destroy(drone.myResourceGO);
    }

    public override string GetName()
    {
        return DroneStates.UnLoading.ToString();
    }
}
