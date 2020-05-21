using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConveyorBuilder))]
[RequireComponent(typeof(ConveyorPoleBuilder))]
[RequireComponent(typeof(ProductionBuildingBuilder))]
public class BuilderManager : MonoBehaviour
{
    [SerializeField] GameObject MyUI;

    List<BuilderBase> mBuilders = new List<BuilderBase>();
    ProductionBuildingBuilder mProductionBuilder = null;

    public enum Action
    {
        none = -1,
        MakingBelt = 0, 
        MakingPole = 1,
        MakingWire = 2,
        MakingElectricPole = 3,
        MakingConveyorSplitter = 4,
        MakingProductionBuilding = 5 //has to be in the end
    }

    int factoryNum = -1;


    Action currentAction = Action.none;


    private void Awake()
    {
        mProductionBuilder = GetComponent<ProductionBuildingBuilder>();
        mBuilders.Add(GetComponent<ConveyorBuilder>());
        mBuilders.Add(GetComponent<ConveyorPoleBuilder>());
        mBuilders.Add(GetComponent<WireBuilder>());
        mBuilders.Add(GetComponent<ElectricPoleBuilder>());
        mBuilders.Add(GetComponent<ConveyorSpliterBuilder>());
        mBuilders.Add(mProductionBuilder);
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
        if(action >= (int)Action.MakingProductionBuilding)
        {
            mProductionBuilder.SetFactory(action - (int)Action.MakingProductionBuilding);
            currentAction = Action.MakingProductionBuilding;
        }
        else
            currentAction = (Action)(action);
    }

}
