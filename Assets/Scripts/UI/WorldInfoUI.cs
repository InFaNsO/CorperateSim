using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldInfoUI : MonoBehaviour
{
    [SerializeField] GameObject player;

    ProductionBuilding currentBuilding;

    [System.Serializable]
    public struct InfoData
    {
        public Image i;
        public TMP_Text txt;
    }
    [SerializeField] List<InfoData> IngredientsImage = new List<InfoData>();
    [SerializeField] InfoData FinalProduct;
    [SerializeField] GameObject MainInfoView;
    [SerializeField] GameObject AlternateInfoView;


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

    void HasRecipie()
    {
        for (int j = 0; j < IngredientsImage.Count; ++j)
        {
            if (j < currentBuilding.recipe.Ingredients.Count)
            {
                IngredientsImage[j].i.sprite = currentBuilding.recipe.Ingredients[j].item.icon;
                IngredientsImage[j].i.color = Color.white;
                IngredientsImage[j].txt.text = currentBuilding.recipe.Ingredients[j].item.name;
            }
            else
            {
                IngredientsImage[j].i.color = new Vector4(0f, 0f, 0f, 0f);
                IngredientsImage[j].txt.text = "";
            }
        }

        FinalProduct.i.sprite = currentBuilding.recipe.finalProduct.item.icon;
        FinalProduct.txt.text = currentBuilding.recipe.finalProduct.item.name;
    }

    void Clear()
    {
        currentBuilding = null;

        MainInfoView.SetActive(false);
        AlternateInfoView.SetActive(false);
    }
}
