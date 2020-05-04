using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricNode : MonoBehaviour
{
    public enum AttachedToBody
    {
        Producer,
        Pole,
        Consumer
    }

    [SerializeField] AttachedToBody body;

    public ElectricNode other;

    void 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (body)
        {
            case AttachedToBody.Producer:
                break;
            case AttachedToBody.Pole:
                break;
            case AttachedToBody.Consumer:
                break;
            default:
                break;
        }
    }
}
