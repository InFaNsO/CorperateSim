using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public ConveyorBeltSegment BeltIn;
    public ConveyorBeltSegment BeltOut;

    [SerializeField] ConveyorInputSlot input;
    [SerializeField] ConveyorOutputSlot output;

    [SerializeField] BuilderManager buildManager;

    private void Awake()
    {

    }

    private void Update()
    {
        if(input.belt && !BeltIn)
            BeltIn = input.belt;
        if (output.belt && !BeltOut)
            BeltOut = output.belt;
    }

}
