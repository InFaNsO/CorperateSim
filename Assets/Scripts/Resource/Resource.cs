using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] public Item item;
    List<OrientedPoint> myPath = new List<OrientedPoint>();
    public ConveyorBeltSegment myCurrentBelt;

    public int pointIndex = -1;

    public bool stopPath = false;

    public float startTime = 0.0f;
    float offset = 1f;

    public Rigidbody myRB = null;
    public float t = 0f;

    public void Start()
    {
        myRB = GetComponent<Rigidbody>();
        Debug.Assert(myRB, "Couldnot find the ridgid body");
    }

    public void ShiftBelt(ConveyorBeltSegment belt)
    {
        myCurrentBelt.RemoveResourceAtIndex(pointIndex);
        myCurrentBelt = belt;
        SetPath();
    }

    public void Update()
    {
        return;
        if (myPath.Count <= 1)
        {
            myRB.velocity = Vector3.zero;
            myRB.angularVelocity = Vector3.zero;

            return;
        }
        if (!myCurrentBelt)
            return;

        if (stopPath)
            return;

        return;

        var opCurr = myPath[0];
        var opNext = myPath[1];
        t = Time.time - startTime / (myCurrentBelt.TimeToReachNextPoint * 2f);

        var p = Vector3.Lerp(opCurr.position, opNext.position, t);
        p.y += offset;
        //transform.position = p;
        //transform.rotation = Quaternion.Slerp(opCurr.orientation, opNext.orientation, t);

        myRB.MovePosition(p);
        myRB.MoveRotation(opNext.orientation);

        //if ((transform.position - myPath[1].position).sqrMagnitude < 1.0f)
        //    myPath.RemoveAt(0);

        if (t >= 1.0f)
            myPath.RemoveAt(0);
    }

    private void LateUpdate()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        var inputSlot = other.GetComponent<ConveyorInputSlot>();
        if(inputSlot)
        { 
            if (inputSlot.GetComponentInParent<ProductionBuilding>())
                inputSlot.GetComponentInParent<ProductionBuilding>().AddResource(gameObject);
            else if (inputSlot.GetComponentInParent<Pole>())
            {
                var pole = inputSlot.GetComponentInParent<Pole>();
                if (pole.BeltOut)
                {
                    myCurrentBelt = pole.BeltOut;
                    SetPath();
                }
            }
            else if (inputSlot.GetComponentInParent<ConveyorSpliter>())
            {
                var spliter = inputSlot.GetComponentInParent<ConveyorSpliter>();
                spliter.SetResourceNextPath(this);
            }
            return;
        }
        var factoryOutput = other.GetComponent<ConveyorOutputSlot>();
        if(factoryOutput)
        {
            //Debug.Log("Created by factory " + factoryOutput.name);
           // myCurrentBelt = factoryOutput.belt;
           // SetPath();
        }
    }

    public void SetPath()
    {
        myCurrentBelt.AddResource(this);
    }
}

