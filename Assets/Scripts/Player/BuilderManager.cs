 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConveyorBuilder))]
[RequireComponent(typeof(ConveyorPoleBuilder))]
[RequireComponent(typeof(ProductionBuildingBuilder))]
[RequireComponent(typeof(FoundationBaseBuilder))]
public class BuilderManager : MonoBehaviour
{
    [SerializeField] GameObject MyUI;

    List<BuilderBase> mBuilders = new List<BuilderBase>();
    ProductionBuildingBuilder mProductionBuilder = null;
    FoundationBaseBuilder mFoundationBaseBuilder = null;
    public enum Action
    {
        none = -1,
        MakingBelt = 0, 
        MakingPole = 1,
        MakingWire = 2,
        MakingElectricPole = 3,
        MakingConveyorSplitter = 4,
        MakingProductionBuilding = 5,
        maxProductionBuilding = 10,
        MakingFoundationBase = 11,
        maxFoundationBase = 12
    }
    Action currentAction = Action.none;


    private void Awake()
    {
        mProductionBuilder = GetComponent<ProductionBuildingBuilder>();
        mFoundationBaseBuilder = GetComponent<FoundationBaseBuilder>();
        mBuilders.Add(GetComponent<ConveyorBuilder>());
        mBuilders.Add(GetComponent<ConveyorPoleBuilder>());
        mBuilders.Add(GetComponent<WireBuilder>());
        mBuilders.Add(GetComponent<ElectricPoleBuilder>());
        mBuilders.Add(GetComponent<ConveyorSpliterBuilder>());
        mBuilders.Add(mProductionBuilder);
        int diff = ((int)Action.maxProductionBuilding - (int)Action.MakingProductionBuilding);
        for (int i = 0; i < diff; ++i)
            mBuilders.Add(new BuilderBase());
        mBuilders.Add(mFoundationBaseBuilder);
        for (int i = 0; i < ((int)Action.maxFoundationBase - (int)Action.MakingFoundationBase); ++i)
            mBuilders.Add(new BuilderBase());
    }

    private void Start()
    {
        EventManager.current.OpenBuildMenu += OpenUI;
        EventManager.current.CloseMenu += CloseUI;
    }

    #region Event Functions
    public void OpenUI()
    {
        MyUI.SetActive(true);
    }

    public void CloseUI()
    {
        MyUI.SetActive(false);
    }
    #endregion

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

    public void SetAction(int action)
    {
        if(action >= (int)Action.MakingProductionBuilding && action <= (int)Action.maxProductionBuilding)
        {
            mProductionBuilder.SetFactory(action - (int)Action.MakingProductionBuilding);
            currentAction = Action.MakingProductionBuilding;
        }
        else if(action >= (int)Action.MakingFoundationBase && action <= (int)Action.maxFoundationBase)
        {
            mFoundationBaseBuilder.SetFoundation(action - (int)Action.MakingFoundationBase);
            currentAction = Action.MakingFoundationBase;
        }
        else
            currentAction = (Action)(action);
    }
}
