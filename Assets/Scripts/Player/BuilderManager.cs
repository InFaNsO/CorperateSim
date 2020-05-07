using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConveyorBuilder))]
[RequireComponent(typeof(ConveyorPoleBuilder))]
[RequireComponent(typeof(ProductionBuildingBuilder))]
public class BuilderManager : MonoBehaviour
{
    [SerializeField] GameObject WireObject;
    [SerializeField] GameObject Extractor;
    [SerializeField] GameObject Convertor;

    List<BuilderBase> mBuilders = new List<BuilderBase>();
    ProductionBuildingBuilder mProductionBuilder = null;

    public enum Action
    {
        none = -1,
        MakingBelt = 0, 
        MakingPole = 1,
        MakingWire = 2,
        MakingElectricPole = 3,
        MakingProductionBuilding = 4    //has to be in the end
    }

    int factoryNum = -1;


    public ConveyorInputSlot slot1;
    public ConveyorOutputSlot slot2;

    public ElectricNode node1;
    public ElectricNode node2;

    Action currentAction = Action.none;


    private void Awake()
    {
        mProductionBuilder = GetComponent<ProductionBuildingBuilder>();
        mBuilders.Add(GetComponent<ConveyorBuilder>());
        mBuilders.Add(GetComponent<ConveyorPoleBuilder>());
        mBuilders.Add(GetComponent<WireBuilder>());
        mBuilders.Add(GetComponent<ElectricPoleBuilder>());
        mBuilders.Add(mProductionBuilder);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAction == Action.none)
            return;

        //If Action
        if (Input.GetButtonDown("Fire1"))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (mBuilders[(int)currentAction].ParseInput(ref r))
            {
                //mBuilders[(int)currentAction].CustomReset();
                currentAction = Action.none;
            }
        }
        //if cancel
        else if (Input.GetButtonDown("Fire2"))
        {
            mBuilders[(int)currentAction].CustomReset();
            currentAction = Action.none;
        }
        //if other action input mech
        else
        {
            mBuilders[(int)currentAction].UpdateObject();
        }

    }

    public void MakeBelt()
    {
        //var belt = Instantiate(ConveyorBelt, Vector3.zero, Quaternion.identity).GetComponent<ConveyorBeltSegment>();
        //belt.ResetPath(slot1.transform, slot2.transform);
        //
        //slot1.belt = belt;
        //slot2.belt = belt;
        //
        //slot1 = null;
        //slot2 = null;
    }

    public void MakeWire()
    {
        //var wire = Instantiate(WireObject, Vector3.zero, Quaternion.identity).GetComponent<Wire>();
        //wire.ResetPath(node1, node2);
        //
        //node1.other = node2;
        //node2.other = node1;
        //
        //node1 = null;
        //node2 = null;
    }

    public void MakeExtractor()
    {
        //var ext = Instantiate(Extractor, Vector3.zero, Quaternion.identity).GetComponent<ProductionBuilding>();
        //ext.SetBuildManager(this);
    }
    public void MakeConvertor()
    {
        //var ext = Instantiate(Convertor, Vector3.zero, Quaternion.identity).GetComponent<ProductionBuilding>();
        //ext.SetBuildManager(this);
    }

    public void SetAction(int action)
    {
        if(action >= (int)Action.MakingProductionBuilding)
        {
            mProductionBuilder.SetFactory(action - (int)Action.MakingProductionBuilding);
            currentAction = Action.MakingProductionBuilding;
        }
        else
            currentAction = (Action)(action);
    }

}
