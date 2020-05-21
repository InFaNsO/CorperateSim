using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ResourceData
{
    public int minResourceCount;
    public int maxResourceCount;
    public Item Resource;
}


public class ResourcePlacer : MonoBehaviour
{
    [SerializeField] GameObject ResourceNode;
    [SerializeField] List<ResourceData> BasicResourceItems = new List<ResourceData>();

    [SerializeField] List<Transform> ResourcesPositions = new List<Transform>();

    //make this use object pool
    List<GameObject> myResources = new List<GameObject>();

    void Start()
    {
        if(myResources.Count > 0)
        {
            foreach (var go in myResources)
            {
                Destroy(go);
            }
        }

        foreach (var resource in BasicResourceItems)
        {

            for (int i = 0; i < resource.minResourceCount; ++i)
            {
                if (ResourcesPositions.Count == 0)
                    return;

                int positionIndex = Random.Range(0, ResourcesPositions.Count);
                var go = Instantiate(ResourceNode, ResourcesPositions[positionIndex]);
                var node = go.GetComponent<ResourceNode>();
                node.mType = resource.Resource;
                node.myRenderer.material = resource.Resource.mat;

                ResourcesPositions.RemoveAt(positionIndex);
                myResources.Add(go);
            }

        }
    }

}
