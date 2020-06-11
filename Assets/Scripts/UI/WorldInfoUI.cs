using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldInfoUI : MonoBehaviour
{
    [SerializeField] GameObject player;

    ProductionBuilding currentBuilding;

    [SerializeField] List<UI_Ingredient> IngredientsImage = new List<UI_Ingredient>();
    [SerializeField] UI_Ingredient FinalProduct;
    [SerializeField] GameObject MainInfoView;
    [SerializeField] GameObject AlternateInfoView;
    [SerializeField] ProgressBar ProducingNext;

    private void Start()
    {
        EventManager.current.BuildingPointed += HandleInfoArea;
        EventManager.current.BuildingNotPointed += Clear;
    }

    public void HandleInfoArea(ProductionBuilding building)
    {
        currentBuilding = building;
        Vector3 forward = -player.transform.forward;
        forward.y = 0f;
        forward.Normalize();
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.position = building.OffsetUI;

        if (currentBuilding.recipe)
        {
            HasRecipie();
            MainInfoView.SetActive(true);
            AlternateInfoView.SetActive(false);
        }
        else
        {
            MainInfoView.SetActive(false);
            AlternateInfoView.SetActive(true);
        }
    }

    public void Update()
    {
        if (!currentBuilding)
            return;
        if (!currentBuilding.recipe)
            return;

        HasRecipie();
    }

    void HasRecipie()
    {
        for (int j = 0; j < IngredientsImage.Count; ++j)
        {
            if (j < currentBuilding.recipe.Ingredients.Count)
            {
                IngredientsImage[j].gameObject.SetActive(true);
                IngredientsImage[j].Icon.sprite = currentBuilding.recipe.Ingredients[j].item.icon;
                IngredientsImage[j].Icon.color = Color.white;
                IngredientsImage[j].Name.text = currentBuilding.recipe.Ingredients[j].item.name;
                IngredientsImage[j].ItemCountBar.Minimum = 0;
                IngredientsImage[j].ItemCountBar.Maximum = (int)currentBuilding.recipe.Ingredients[j].item.maxQuantity;
                IngredientsImage[j].ItemCountBar.Current = (int)currentBuilding.InputSlots[j].currentQuantity;

            }
            else
            {
                IngredientsImage[j].gameObject.SetActive(false);
                IngredientsImage[j].Icon.color = new Vector4(0f, 0f, 0f, 0f);
                IngredientsImage[j].Name.text = "";
            }
        }

        FinalProduct.Icon.sprite = currentBuilding.recipe.finalProduct.item.icon;
        FinalProduct.Name.text = currentBuilding.recipe.finalProduct.item.name;
        FinalProduct.ItemCountBar.Minimum = 0;
        //get these number from someotherplace
        FinalProduct.ItemCountBar.Maximum = (int)currentBuilding.recipe.finalProduct.item.maxQuantity;
        FinalProduct.ItemCountBar.Current = (int)currentBuilding.OutPutSlot.currentQuantity;

        ProducingNext.Minimum = 0;
        ProducingNext.Maximum = 100;
        float t = (currentBuilding.recipe.ProduceNext - Time.time) / (float)currentBuilding.recipe.TimeToProduce1;
        t = 1f - t;
        ProducingNext.Current = (int)(t * 100f);
    }

    void Clear()
    {
        currentBuilding = null;

        MainInfoView.SetActive(false);
        AlternateInfoView.SetActive(false);
    }
}
