using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorOutputSlot : MonoBehaviour
{
    public ConveyorBeltSegment belt = null;
    public GameObject Arrow = null;

    private void Update()
    {
        if (Arrow && belt)
            if (Arrow.activeSelf)
                Arrow.SetActive(false);

    }
}
