using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Accessibility;
public class EventManager : MonoBehaviour
{
    public static EventManager current;
    UIInputs uiInputs;
     public Transform PlayerTransformation;

    bool mBuildingMenu = false;
    bool mBuildMenu = false;
    bool mCloseMenu = false;
    bool mDestroyObjects = false;

    bool mUiOpened = false;

    #region Setup
    void Awake()
    {
        uiInputs = new UIInputs();
        current = this;
        OpenBuildMenu += OnMenueOpen;
        OpenBuildingInfoMenu += OnMenueOpen;
        CloseMenu += TurnAllFlagsOff;
        CloseMenu += OnMenueClose;
    }
    private void Start()
    {
        uiInputs.UIActions.BuildingMenue.started    += ctx => mBuildingMenu     = true;
        uiInputs.UIActions.BuildingMenue.canceled   += ctx => mBuildingMenu     = false;
        uiInputs.UIActions.BuildMenu.started        += ctx => mBuildMenu        = true;
        uiInputs.UIActions.BuildMenu.canceled       += ctx => mBuildMenu        = false;
        uiInputs.UIActions.CloseMenu.started        += ctx => mCloseMenu        = true;
        uiInputs.UIActions.CloseMenu.canceled       += ctx => mCloseMenu        = false;
        uiInputs.UIActions.DestroyObject.started    += ctx => mDestroyObjects   = true;
        uiInputs.UIActions.DestroyObject.canceled   += ctx => mDestroyObjects   = false;
    }

    private void OnEnable()
    {
        uiInputs.Enable();
    }
    private void OnDisable()
    {
        uiInputs.Disable();
    }
    #endregion
    
    
    #region Actions
    public event Action OpenBuildMenu;
    public event Action<ProductionBuilding> OpenBuildingInfoMenu;
    public event Action CloseMenu;
    public event Action DestroyObject;
    #endregion

    void Update()
    {
        if (mBuildMenu && !mUiOpened)
            OpenBuildMenu?.Invoke();
        if (mBuildingMenu && !mUiOpened)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(PlayerTransformation.position, PlayerTransformation.forward,
                out hitInfo, float.MaxValue, LayerMask.GetMask("ProductionBuilding")))
            {
                var building = hitInfo.collider.GetComponentInParent<ProductionBuilding>();
                if (building)
                    OpenBuildingInfoMenu?.Invoke(building);
            }
        }
        if (mDestroyObjects && !mUiOpened)
        {
            DestroyObject?.Invoke();
        }
        if (mCloseMenu && mUiOpened)
            CloseMenu?.Invoke();
    }


    #region ActionFunctions
    public void OnCloseMenue()
    {
        CloseMenu?.Invoke();
    }
    public void OnBuildingInfoMenu(ProductionBuilding b)
    {
        OpenBuildingInfoMenu?.Invoke(b  );
    }
    #endregion


    #region My Action Funtions
    void TurnAllFlagsOff()
    {
        mBuildMenu = false;
        mBuildingMenu = false;
        mCloseMenu = false;
        mDestroyObjects = false;
    }

    void OnMenueOpen(ProductionBuilding b)
    {
        OnMenueOpen();
    }
    void OnMenueOpen()
    {
        mUiOpened = true;
        TurnAllFlagsOff();
    }

    void OnMenueClose()
    {
        mUiOpened = false;
    }
    #endregion
}
