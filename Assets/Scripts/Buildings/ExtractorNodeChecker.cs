using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractorNodeChecker : MonoBehaviour
{
    [SerializeField] ResourceNode node;
    [HideInInspector] public ProductionBuilding myBuilding = null;

    private void OnTriggerEnter(Collider other)
    {
        var extractor = other.GetComponentInParent<ProductionBuilding>();
        if (extractor && extractor.MachineType == ProductionMachine.Extractor)
        {
            if (myBuilding == null)
            {
                myBuilding = extractor;
                extractor.SetRecipie(node.mType);
            }
        }
    }
}
