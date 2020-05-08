using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ConveyorSpliter : MonoBehaviour
{
    [SerializeField] ConveyorInputSlot inputSlot;
    [SerializeField] ConveyorOutputSlot outputSlotFront;
    [SerializeField] ConveyorOutputSlot outputSlotLeft;
    [SerializeField] ConveyorOutputSlot outputSlotRight;

    enum Direction
    {
        Left,
        Front,
        Right
    }

    Direction nextResourceDirection = Direction.Left;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResourceNextPath(Resource resource)
    {
        resource.myCurrentBelt = null;
        int count = 0;
        while (count < 2)
        {
            if (nextResourceDirection == Direction.Left)
            {
                if (outputSlotLeft.belt)
                    resource.myCurrentBelt = outputSlotLeft.belt;
                nextResourceDirection = Direction.Front;
            }
            if (nextResourceDirection == Direction.Front && !resource.myCurrentBelt)
            {
                if (outputSlotFront.belt)
                    resource.myCurrentBelt = outputSlotFront.belt;
                nextResourceDirection = Direction.Right;
            }
            if (nextResourceDirection == Direction.Right && !resource.myCurrentBelt)
            {
                if (outputSlotRight.belt)
                    resource.myCurrentBelt = outputSlotRight.belt;
                nextResourceDirection = Direction.Left;
            }

            count++;
        }
        if(resource.myCurrentBelt)
            resource.SetPath();
    }
}
