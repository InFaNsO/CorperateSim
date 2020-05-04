using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricConsumer : MonoBehaviour
{
    [SerializeField] public int PowerCunsuption = 10;
    [SerializeField] ElectricNode myNode;

    ProductionBuilding myBuilding = null;
    public bool echoed = false;
    bool hasElectricity = false;

    private void Awake()
    {
        myBuilding = GetComponent<ProductionBuilding>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasElectricity && myNode.other)
        {
            hasElectricity = myNode.other.HavePower() && myNode.other.GetPower(-PowerCunsuption);
            if (hasElectricity)
                myBuilding.TurnOn();
        }
    }

    public void ShutDown()
    {
        myBuilding.ShutDown();
        hasElectricity = false;
    }
}
