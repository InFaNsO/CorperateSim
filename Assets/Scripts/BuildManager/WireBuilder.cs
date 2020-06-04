using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBuilder : BuilderBase
{
    ElectricNode node1 = null;
    ElectricNode node2 = null;

    int NodeMask = 0;
    int GroundMask = 0;
    float playerRange = float.MaxValue;

    ElectricPoleBuilder mPoleMaker = null;
    GameObject currentWire = null;

    private void Awake()
    {
        mPoleMaker = GetComponent<ElectricPoleBuilder>();

        NodeMask = LayerMask.GetMask("ElectricNode");
        GroundMask = LayerMask.GetMask("Ground");
    }

    public override bool ParseInput(ref Ray mouseRay)
    {
        if (node1 && node2)
            return MakeWire();

        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, playerRange, NodeMask))
        {
            if (node1)
            {
                node2 = hit.collider.GetComponent<ElectricNode>();
                if (node1 == node2 && !node2.other)
                    node2 = null;
            }
            else
            {
                node1 = hit.collider.GetComponent<ElectricNode>();
                if (node1.other)
                    node1 = null;
            }
            return MakeWire();
        }

        if(mPoleMaker.ParseInput(ref mouseRay))
        {
            node1 = null;
            mPoleMaker.currentPole = null;
            if (currentWire)
                currentWire = null;
            return true;
        }

        return false;
    }
    public override void CustomReset()
    {
        node1 = null;
        node2 = null;

        if (currentWire)
            Destroy(currentWire);

        mPoleMaker.CustomReset();
    }
    private bool MakeWire()
    {
        if (node1 == null || node2== null)
            return false;

        var wire = Instantiate(buildObject, Vector3.zero, Quaternion.identity).GetComponent<Wire>();
        wire.ResetPath(node1, node2);

        node1.other = node2;
        node2.other = node1;

        CustomReset();

        return true;
    }

    public override void UpdateObject()
    {
        if (!node1 && !node2)
            return;

        RaycastHit info;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (node1)
        {
            if (Physics.Raycast(r, out info, playerRange, NodeMask))
            {
                MakeTempWire(node1, info.collider.GetComponent<ElectricNode>());
                mPoleMaker.CustomReset();
            }
            else if (Physics.Raycast(r, out info, playerRange, GroundMask))
            {
                mPoleMaker.UpdateObject();
                if (mPoleMaker.currentPole)
                    MakeTempWire(node1, mPoleMaker.currentPole.GetComponent<ElectricPole>().myNodes[0]);
            }
        }
    }

    void MakeTempWire(ElectricNode start, ElectricNode end)
    {
        if (currentWire)
            Destroy(currentWire);

        currentWire = Instantiate(buildObject, Vector3.zero, Quaternion.identity);
        var b = currentWire.GetComponent<Wire>();
        b.ResetPath(start, end);
    }
}
