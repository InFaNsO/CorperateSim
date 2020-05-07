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
        var factoryInput = other.GetComponent<ConveyorInputSlot>();
        if(factoryInput)
        {
            if(factoryInput.GetComponentInParent<ProductionBuilding>())
                factoryInput.GetComponentInParent<ProductionBuilding>().AddResource(gameObject);
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

