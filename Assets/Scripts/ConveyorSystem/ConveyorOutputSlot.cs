using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorOutputSlot : MonoBehaviour
{
    [SerializeField] public BuilderManager bm;
    public ConveyorBeltSegment belt = null;
    private void OnMouseOver()
    {
        if (bm.slot1 != null && Input.GetMouseButtonDown(0) && !belt)
        {
            bm.slot2 = this;
            bm.MakeBelt();
        }

    }
}
