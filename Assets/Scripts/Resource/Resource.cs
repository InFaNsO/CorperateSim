using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] public Item item;
    List<OrientedPoint> myPath = new List<OrientedPoint>();
    public ConveyorBeltSegment myCurrentBelt;

    float startTime = 0.0f;
    float offset = 1f;

    public void Start()
    {

    }

    public void Update()
    {
        if (myPath.Count <= 1)
        {
            if (myCurrentBelt)
            {
                var pole = myCurrentBelt.startPos.GetComponentInParent<Pole>(); //chnage this later
                if (pole)
                {
                    if (pole.BeltOut)
                    {
                        myCurrentBelt = pole.BeltOut;
                        SetPath();
                    }
                }
            }
            return;
        }
        if (!myCurrentBelt)
            return;

        var opCurr = myPath[0];
        var opNext = myPath[1];
        float t = Time.time - startTime / myCurrentBelt.TimeToReachNextPoint;

        var p = Vector3.Lerp(opCurr.position, opNext.position, t);
        p.y += offset;
        transform.position = p;
        transform.rotation = Quaternion.Slerp(opCurr.orientation, opNext.orientation, t);

        if (t >= 1.0f)
            myPath.RemoveAt(0);
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
        var p = myCurrentBelt.path.CalculateEvenlySpaceOrientedPoints(0.1f);
        for(int i = 0; i < p.Length; ++i)
        {
            myPath.Add(p[i]);
        }

        startTime = Time.time;
    }
}

