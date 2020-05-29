using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    GameObject current;

    List<int> layerMasks = new List<int>();
    [SerializeField] string[] names = { "ConveyorBelt", "ProductionBuilding", "FoundationEdge" };
    [SerializeField] TMP_Text ObjectNameUI;

    float playerRange = float.MaxValue;

    private void Awake()
    {

        for (int i = 0; i < names.Length; ++i)
            layerMasks.Add(LayerMask.GetMask(names[i]));
    }
    private void Start()
    {
        EventManager.current.DestroyObject += OnDestroyStart;
    }

    public void OnDestroyStart()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        for (int i = 0; i < layerMasks.Count; ++i)
        {
            if (Physics.Raycast(mouseRay, out hit, playerRange, layerMasks[i]))
            {
                current = hit.collider.gameObject;
                if(names[i] == "ConveyorBelt")
                {

                }
                else if(names[i] == "ProductionBuilding")
                {
                    var building = current.GetComponentInParent<ProductionBuilding>();
                    current = building.gameObject;
                }
                else if(names[i] == "FoundationEdge")
                {
                    var foundation = current.GetComponentInParent<Foundation>();
                    current = foundation.gameObject;
                }
                Destroy(current);
            }
        }
    }

}
