using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Item_ProductionBuilding : MonoBehaviour
{
    UI_Item myItem = null;
    UIProductionBuilding ui = null;
    private void Awake()
    {
        myItem = GetComponent<UI_Item>();
    }

    public void Set(Recipe r, UIProductionBuilding b)
    {
        ui = b;

        myItem.myImage.sprite = r.finalProduct.item.icon;
        myItem.myButton.buttonText = r.finalProduct.item.name;
        myItem.myItem = r.finalProduct.item;

        myItem.myButton.buttonEvent.AddListener(ButtonClick);
    }

    public void ButtonClick()
    {
        ui.OnRecipieSelect(myItem.myItem);
    }
}
