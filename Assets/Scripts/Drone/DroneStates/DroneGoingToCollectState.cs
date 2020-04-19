using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneGoingToCollectState : State
{
    Drone drone;
    NavMeshAgent myAgent;
    

    // Start is called before the first frame update
    void Start()
    {
        drone = GetComponentInParent<Drone>();
        myAgent = GetComponentInParent<NavMeshAgent>();
    }

    public override void Enter()
    {
        Debug.Log("Entered going to collect state");
        myAgent.SetDestination(drone.CollectionPoint.position);
    }

    public override void MyUpdate()
    {
        if(drone.myCurrentNode != null)
        {
            drone.myStatemachine.ChangeState(DroneStates.Loading.ToString());
        }
    }

    public override void Exit()
    {
    }

    public override string GetName()
    {
        return DroneStates.GoingToCollect.ToString();
    }
}
