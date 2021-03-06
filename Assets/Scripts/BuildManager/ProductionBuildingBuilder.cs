﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuildingBuilder : BuilderBase
{
    [SerializeField] float rotationSpeed = 2.0f;

    [SerializeField] List<GameObject> FactoryObjects = new List<GameObject>();

    int GroundMask = 0;
    int NodeMask = 0;
    int FactoryIndex = 0;

    float playerRange = float.MaxValue;

    GameObject currentFactory = null;
    ExtractorNodeChecker prvNChecker = null;

    private void Awake()
    {
        GroundMask = LayerMask.GetMask("Ground"); //add structures mask aswell
        NodeMask = LayerMask.GetMask("ResourceNode");
    }

    public void SetFactory(int index)
    {
        FactoryIndex = index;
    }

    public override bool ParseInput(ref Ray mouseRay)
    {
        if (currentFactory)
        {
            currentFactory = null;
            return true;
        }
        return false;
    }

    public override void CustomReset()
    {
        if(currentFactory)
            Destroy(currentFactory);
    }

    public override void UpdateObject()
    {
        if (!currentFactory)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (FactoryIndex == 0)
            {
                if (Physics.Raycast(mouseRay, out hit, playerRange, NodeMask))
                {
                    var ncheck = hit.collider.GetComponent<ExtractorNodeChecker>();
                    if (!ncheck.myBuilding)
                    {
                        prvNChecker = ncheck;
                        currentFactory = Instantiate(FactoryObjects[FactoryIndex], hit.collider.transform.position, Quaternion.identity);
                    }
                }
            }
            else if (Physics.Raycast(mouseRay, out hit, playerRange, GroundMask))
            {
                currentFactory = Instantiate(FactoryObjects[FactoryIndex], hit.point, Quaternion.identity);
            }
        }

        if (!currentFactory)
            return;

        var d = Input.GetAxis("MouseScroll");
        if (d != 0f)
            currentFactory.transform.Rotate(currentFactory.transform.up, rotationSpeed * d);

        if (FactoryIndex == 0)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, playerRange, NodeMask))
            {
                var ncheck = hit.collider.GetComponent<ExtractorNodeChecker>();
                if (!ncheck.myBuilding)
                {
                    prvNChecker.myBuilding = null;
                    prvNChecker = ncheck;
                    currentFactory.transform.position = hit.collider.transform.position;
                }
            }
            return;
        }

        Ray mouseR = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;
        if (Physics.Raycast(mouseR, out h, playerRange, GroundMask))
        {
            currentFactory.transform.position = h.point;
        }

    }
}
