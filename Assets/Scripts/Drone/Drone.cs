using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] public Transform DropPoint = null;
    [SerializeField] public Transform CollectionPoint = null;
    [SerializeField] public Transform ResourcePoint;

    [SerializeField] public float MaxCarryCapacity = 20.0f;

    public float mCurrentCapacity = 0.0f;

    [HideInInspector] public GameObject myResourceGO = null;
    [HideInInspector] public Resource myCarryingResource = null;
    [HideInInspector] public StateMachine myStatemachine = null;
    [HideInInspector] public UnityEngine.AI.NavMeshAgent  myAgent = null;

    [HideInInspector] public ResourceNode myCurrentNode = null;
    [HideInInspector] public Factory myCurrentFactoryDropOff = null;
    [HideInInspector] public Factory myCurrentFactoryPickUp = null;

    [HideInInspector] public bool ResourcesGiven = false;
    [HideInInspector] public bool ResourcesTaken = false;

    //[HideInInspector] public 

    // Start is called before the first frame update
    void Start()
    {
        myStatemachine = GetComponent<StateMachine>();
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var node = other.GetComponent<ResourceNode>();
        if (node)
        {
            Debug.Log("Touched a node");
            myCurrentNode = node;
            return;
        }
        var factoryParcer = other.GetComponent<FactoryColliderParcer>();
        if (factoryParcer)
        {
            switch (factoryParcer.myLocation)
            {
                case FactoryColliderLocation.Entry:
                    Debug.Log("Entered a Factory enterance");
                    myCurrentFactoryDropOff = other.GetComponentInParent<Factory>();
                    break;
                case FactoryColliderLocation.Exit:
                    Debug.Log("Entered a Factory Exit");
                    myCurrentFactoryPickUp = other.GetComponentInParent<Factory>();
                    break;
                default:
                    break;
            }
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var node = other.GetComponent<ResourceNode>();
        if (node)
        {
            Debug.Log("Exited a node in trigger");
            myCurrentNode = null;
        }
        var factoryParcer = other.GetComponent<FactoryColliderParcer>();
        if (factoryParcer)
        {
            switch (factoryParcer.myLocation)
            {
                case FactoryColliderLocation.Entry:
                    Debug.Log("Exited a Factory enterance");
                    myCurrentFactoryDropOff = null;
                    break;
                case FactoryColliderLocation.Exit:
                    Debug.Log("Exited a Factory Exit");
                    myCurrentFactoryPickUp = null;
                    break;
                default:
                    break;
            }
            return;
        }
    }

    public float GetMaxCapacity() { return MaxCarryCapacity; }
    public float CurrentCapacity() { return mCurrentCapacity; }

    public Resource GetResource() { return myCarryingResource; }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(DropPoint)
            Gizmos.DrawWireSphere(DropPoint.position, 3.0f);
        if(CollectionPoint)
            Gizmos.DrawWireSphere(CollectionPoint.position, 3.0f);

        Gizmos.color = Color.green;
        if(myAgent)
            Gizmos.DrawWireSphere(myAgent.destination, 5.0f);
    }
}
