using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorInputSlot : MonoBehaviour
{
    [SerializeField] public BuilderManager bm;
    public ConveyorBeltSegment belt = null;

    private void OnMouseOver()
    {
        if(bm.slot1 == null && Input.GetMouseButtonDown(0) && !belt)
        {
            bm.slot1 = this;
        }

    }
}
