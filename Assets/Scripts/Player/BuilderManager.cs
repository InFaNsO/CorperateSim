using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    [SerializeField] GameObject ConveyorBelt;
    [SerializeField] GameObject WireObject;

    public enum Action
    {
        none,
        MakingBelt
    }


    public ConveyorInputSlot slot1;
    public ConveyorOutputSlot slot2;

    public ElectricNode node1;
    public ElectricNode node2;

    Action currentAction = Action.none;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
        }
    }

    public void MakeBelt()
    {
        var belt = Instantiate(ConveyorBelt, Vector3.zero, Quaternion.identity).GetComponent<ConveyorBeltSegment>();
        belt.ResetPath(slot1.transform, slot2.transform);

        slot1.belt = belt;
        slot2.belt = belt;

        slot1 = null;
        slot2 = null;
    }

    public void MakeWire()
    {
        var wire = Instantiate(WireObject, Vector3.zero, Quaternion.identity).GetComponent<Wire>();
        wire.ResetPath(node1, node2);

        node1.other = node2;
        node2.other = node1;

        node1 = null;
        node2 = null;
    }
}
