using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricProducer : MonoBehaviour
{
    [SerializeField] public int PowerProduction = 10;
    [SerializeField] ElectricNode myNode;

    public bool echoed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!echoed && myNode.other)
        {
            AddPower();
            echoed = true;
        }
    }

    public void AddPower()
    {
        myNode.other.AddPower(PowerProduction);
    }

    public void ShutDouwn()
    {
         
    }
}