using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBase : MonoBehaviour
{
    [SerializeField] protected GameObject buildObject;


    public virtual bool ParseInput(ref Ray mouseRay)
    {
        return false;
    }
    public virtual void CustomReset()
    {
    }

    public virtual void UpdateObject()
    {

    }
}
