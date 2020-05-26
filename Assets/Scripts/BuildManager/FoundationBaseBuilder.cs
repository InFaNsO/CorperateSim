using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationBaseBuilder : BuilderBase
{
    [SerializeField] List<GameObject> mBaseFoundationPrefabs = new List<GameObject>();
    GameObject currentBase = null;

    int GroundMask = 0;
    int FoundationMask = 0;

    int foundationIndex = 0;
    float playerRange = float.MaxValue;
    float rotationSpeed = 2.0f;

    void Awake()
    {
        GroundMask = LayerMask.GetMask("Ground");
        FoundationMask = LayerMask.GetMask("FoundationEdge");

    }

    public void SetFoundation(int i)
    {
        foundationIndex = i;
    }

    public override bool ParseInput(ref Ray mouseRay)
    {
        //because you can build it until u cancel
        if (currentBase)
        {
            currentBase = null;
            //return true;
        }
        return false;
    }

    public override void CustomReset()
    {
        if (currentBase)
            Destroy(currentBase);
    }

    void MoveToEdge(Collider col)
    {
        Vector3 dir = col.transform.position - col.GetComponentInParent<Foundation>().transform.position;
        currentBase.transform.position = col.transform.position + dir;
    }

    public override void UpdateObject()
    {
        if(!currentBase)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, playerRange, FoundationMask))
            {
                currentBase = Instantiate(mBaseFoundationPrefabs[foundationIndex], hit.point, hit.collider.GetComponentInParent<Foundation>().transform.rotation);
                MoveToEdge(hit.collider);
            }
            else if (Physics.Raycast(mouseRay, out hit, playerRange, GroundMask))
            {
                currentBase = Instantiate(mBaseFoundationPrefabs[foundationIndex], hit.point, Quaternion.identity);
            }
        }

        if (!currentBase)
            return;

        Ray mouseR = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;
        if (Physics.Raycast(mouseR, out h, playerRange, FoundationMask))
        {
            MoveToEdge(h.collider);
            currentBase.transform.rotation = h.collider.GetComponentInParent<Foundation>().transform.rotation;
        }
        else if(Physics.Raycast(mouseR, out h, playerRange, GroundMask))
        {
            var f = h.collider.GetComponentInParent<Foundation>();
            if(!f || f.GetInstanceID() != currentBase.GetComponent<Foundation>().GetInstanceID())
                currentBase.transform.position = h.point;
            var d = Input.GetAxis("MouseScroll");
            if (d != 0f)
                currentBase.transform.Rotate(currentBase.transform.up, rotationSpeed * d);
        }
    }
}
