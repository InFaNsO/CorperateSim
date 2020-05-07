using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ConveyorBuilder : BuilderBase
{
    ConveyorInputSlot slot1 = null;
    ConveyorOutputSlot slot2 = null;

    int InputMask = 0;
    int OutputMask = 0;
    int GroundMask = 0;

    float playerRange = float.MaxValue;

    GameObject currentBelt;

    ConveyorPoleBuilder mPoleMaker = null;

    private void Awake()
    {
        mPoleMaker = GetComponent<ConveyorPoleBuilder>();

        InputMask = LayerMask.GetMask("ConveyorInput");
        OutputMask = LayerMask.GetMask("ConveyorOutput");
        GroundMask = LayerMask.GetMask("Ground"); //add structures mask aswell
    }

    public override bool ParseInput(ref Ray mouseRay)
    {
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, playerRange, InputMask))
        {
            slot1 = hit.collider.GetComponent<ConveyorInputSlot>();
            if (slot1.belt)
                slot1 = null;
            return MakeBelt();
        }
        else if (Physics.Raycast(mouseRay, out hit, playerRange, OutputMask))
        {
            slot2 = hit.collider.GetComponent<ConveyorOutputSlot>();
            if (slot2.belt)
                slot2 = null; 
            return MakeBelt();
        }
        if(mPoleMaker.currentPole)
        {
            var belt = currentBelt.GetComponent<ConveyorBeltSegment>();

            if (slot1)
            {
                slot1.belt = belt;
                mPoleMaker.currentPole.GetComponentInChildren<ConveyorOutputSlot>().belt = belt;
            }
            else if (slot2)
            {
                slot2.belt = belt;
                mPoleMaker.currentPole.GetComponentInChildren<ConveyorInputSlot>().belt = belt;
            }
            slot1 = null;
            slot2 = null;
            mPoleMaker.currentPole = null;
            if (currentBelt)
                currentBelt = null;

            return true;
        }
        return false;
    }

    public override void CustomReset()
    {
        slot1 = null;
        slot2 = null;

        if (currentBelt)
            Destroy(currentBelt);

        mPoleMaker.CustomReset();
    }

    bool MakeBelt()
    {
        if (slot1 == null || slot2 == null)
            return false;

        if (currentBelt)
            Destroy(currentBelt);

        var belt = Instantiate(buildObject, Vector3.zero, Quaternion.identity).GetComponent<ConveyorBeltSegment>();
        belt.ResetPath(slot1, slot2);

        slot1.belt = belt;
        slot2.belt = belt;

        mPoleMaker.currentPole = null;

        CustomReset();

        return true;
    }

    public override void UpdateObject()
    {
        if (!slot1 && !slot2)
            return;

        RaycastHit infoInput, infoOutput, infoGround;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool inputColliding = Physics.Raycast(r, out infoInput, playerRange, InputMask);
        bool outputColliding = Physics.Raycast(r, out infoOutput, playerRange, OutputMask);
        bool groundColliding = Physics.Raycast(r, out infoGround, playerRange, GroundMask);

        if (slot1)
        {
            if (outputColliding)
            {
                MakeTempBelt(slot1, infoOutput.collider.GetComponent<ConveyorOutputSlot>());
                mPoleMaker.CustomReset();
            }
            else if (groundColliding)
            {
                mPoleMaker.UpdateObject();
                if (mPoleMaker.currentPole)
                    MakeTempBelt(slot1, mPoleMaker.currentPole.GetComponentInChildren<ConveyorOutputSlot>());
            }
            return;
        }
        else if (slot2)
        {
            if (inputColliding)
            {
                MakeTempBelt(infoInput.collider.GetComponent<ConveyorInputSlot>(), slot2);
                mPoleMaker.CustomReset();
            }
            else if (groundColliding)
            {
                mPoleMaker.UpdateObject();
                if (mPoleMaker.currentPole)
                    MakeTempBelt(mPoleMaker.currentPole.GetComponentInChildren<ConveyorInputSlot>(), slot2);
            }
            return;
        }
    }

    void MakeTempBelt(ConveyorInputSlot start, ConveyorOutputSlot end)
    {
        if (currentBelt)
            Destroy(currentBelt);

        currentBelt = Instantiate(buildObject, Vector3.zero, Quaternion.identity);
        var b = currentBelt.GetComponent<ConveyorBeltSegment>();
        b.ResetPath(start, end);
    }
}
