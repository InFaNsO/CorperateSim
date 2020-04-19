using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLoadingState : State
{
    Drone drone;

    void Start()
    {
        drone = GetComponentInParent<Drone>();
    }


    public override void Enter()
    {
        Debug.Log("Entered Loading State");
        drone.myAgent.SetDestination(drone.transform.position);
        drone.myResourceGO = Instantiate(drone.myCurrentNode.GetResourceObject(), drone.ResourcePoint);
        drone.myCarryingResource = drone.myResourceGO.GetComponent<Resource>();
    }

    public override void MyUpdate()
    {
        if(drone.MaxCarryCapacity <= drone.mCurrentCapacity)
        {
            drone.myStatemachine.ChangeState(DroneStates.GoingToDropOff.ToString());
            return;
        }

        drone.mCurrentCapacity += drone.myCurrentNode.GetResource(drone.MaxCarryCapacity);
    }

    public override void Exit()
    {
        drone.myCurrentNode = null;
    }

    public override string GetName()
    {
        return DroneStates.Loading.ToString();
    }
}
