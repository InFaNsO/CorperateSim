using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractorNodeChecker : MonoBehaviour
{
    [SerializeField] ResourceNode node;

    private void OnTriggerEnter(Collider other)
    {
        var extractor = other.GetComponentInParent<ExtractorBuilding>();
        if(extractor)
            extractor.NodeResourceSetup(node);
    }
}
