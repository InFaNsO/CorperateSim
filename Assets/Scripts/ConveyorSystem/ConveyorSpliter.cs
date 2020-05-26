using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ConveyorSpliter : MonoBehaviour
{
    [SerializeField] ConveyorInputSlot inputSlot = null;
    [SerializeField] ConveyorOutputSlot outputSlotFront = null;
    [SerializeField] ConveyorOutputSlot outputSlotLeft = null;
    [SerializeField] ConveyorOutputSlot outputSlotRight = null;

    ConveyorBeltSegment nextBelt = null;

    void Update()
    {
        if (nextBelt)
            return;
        if (outputSlotLeft.belt)
            nextBelt = outputSlotLeft.belt;
        else if (outputSlotFront.belt)
            nextBelt = outputSlotFront.belt;
        else if (outputSlotRight.belt)
            nextBelt = outputSlotRight.belt;
    }

    void UpdateBelt()
    {
        if(nextBelt == outputSlotLeft.belt)
        {
            nextBelt = outputSlotFront.belt ? outputSlotFront.belt : outputSlotRight.belt ? outputSlotRight.belt : nextBelt;
        }
        else if (nextBelt == outputSlotFront.belt)
        {
            nextBelt = outputSlotRight.belt ? outputSlotFront.belt : outputSlotLeft.belt ? outputSlotLeft.belt : nextBelt;
        }
        else if (nextBelt == outputSlotRight.belt)
        {
            nextBelt = outputSlotLeft.belt ? outputSlotLeft.belt : outputSlotFront.belt ? outputSlotFront.belt : nextBelt;
        }
    }

    public void SetResourceNextPath(Resource resource)
    {
        if (!nextBelt || !resource)
            return;

        if(!nextBelt.HasResource())
            resource.ShiftBelt(nextBelt);
        UpdateBelt();
    }
}
