using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] public Item item;
    List<OrientedPoint> myPath = new List<OrientedPoint>();
    public ConveyorBeltSegment myCurrentBelt;

    public bool stopPath = false;

    float startTime = 0.0f;
    float offset = 1f;

    Rigidbody myRB = null;

    public void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    public void Update()
    {
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

        var opCurr = myPath[0];
        var opNext = myPath[1];
        float t = Time.time - startTime / (myCurrentBelt.TimeToReachNextPoint * 2f);

        var p = Vector3.Lerp(opCurr.position, opNext.position, t);
        p.y += offset;
        transform.position = p;
        transform.rotation = Quaternion.Slerp(opCurr.orientation, opNext.orientation, t);

        //myRB.MovePosition(p);
        //myRB.MoveRotation(opNext.orientation);

        //if ((transform.position - myPath[1].position).sqrMagnitude < 1.0f)
        //    myPath.RemoveAt(0);

        if (t >= 1.0f)
            myPath.RemoveAt(0);
    }

    private void LateUpdate()
    {
        if (myCurrentBelt)
        {
            var maxV = myCurrentBelt.maxVel;
            Vector3 vel = myRB.velocity;
            vel.x = Mathf.Clamp(vel.x, -maxV, maxV);
            vel.y = Mathf.Clamp(vel.y, -maxV, maxV);
            vel.z = Mathf.Clamp(vel.z, -maxV, maxV);
            myRB.velocity = vel;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var inputSlot = other.GetComponent<ConveyorInputSlot>();
        if(inputSlot)
        {
            if(inputSlot.GetComponentInParent<ProductionBuilding>())
                inputSlot.GetComponentInParent<ProductionBuilding>().AddResource(gameObject);
            else if(inputSlot.GetComponentInParent<Pole>())
            {
                var pole = inputSlot.GetComponentInParent<Pole>();
                if(pole.BeltOut)
                {
                    myCurrentBelt = pole.BeltOut;
                    SetPath();
                }
            }
            else if(inputSlot.GetComponentInParent<ConveyorSpliter>())
            {
                var spliter = inputSlot.GetComponentInParent<ConveyorSpliter>();
                spliter.SetResourceNextPath(this);
            }

            return;
        }
        var factoryOutput = other.GetComponent<ConveyorInputSlot>();
        if(factoryOutput)
        {
            Debug.Log("Created by factory " + factoryOutput.name);
           // myCurrentBelt = factoryOutput.belt;
           // SetPath();
        }
    }

    public void SetPath()
    {
        myPath.Clear();
        var p = myCurrentBelt.myPoints;//path.CalculateEvenlySpaceOrientedPoints(0.1f);
        for(int i = 0; i < p.Length; ++i)
        {
            myPath.Add(p[i]);
        }

        startTime = Time.time;
    }
}

