using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneGoingToDropOffState : State
{
    Drone myDrone;
    NavMeshAgent myAgent;


    // Start is called before the first frame update
    void Start()
    {
        myDrone = GetComponentInParent<Drone>();
        myAgent = GetComponentInParent<NavMeshAgent>();
    }


    public override void Enter()
    {
        Debug.Log("Entered going to collect state");
        myAgent.SetDestination(myDrone.DropPoint.position);
    }

    public override void MyUpdate()
    {
        /*if (myDrone.myCurrentFactoryDropOff != null)
        {
            myDrone.myStatemachine.ChangeState(DroneStates.UnLoading.ToString());
        }*/
    }

    public override void Exit()
    {
    }

    public override string GetName()
    {
        return DroneStates.GoingToDropOff.ToString();
    }
}
