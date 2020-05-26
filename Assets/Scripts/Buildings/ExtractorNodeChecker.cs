using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractorNodeChecker : MonoBehaviour
{
    [SerializeField] ResourceNode node;

    private void OnTriggerEnter(Collider other)
    {
        var extractor = other.GetComponentInParent<ProductionBuilding>();
        if (extractor && extractor.MachineType == ProductionMachine.Extractor)
        {
            extractor.SetRecipie(node.mType);
        }
    }
}
