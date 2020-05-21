using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProductionBuilding : MonoBehaviour
{
    [SerializeField] GameObject MyUI;
    [SerializeField] RectTransform PopulationArea;
    [SerializeField] GameObject Parent;
    [SerializeField] GameObject Icon;



    public ProductionBuilding currentBuilding;
    public List<GameObject> objects;

    private void Start()
    {
        EventManager.current.OpenBuildingInfoMenu += OpenUI;
        EventManager.current.CloseMenu += CloseUI;
        if (currentBuilding)
            EventManager.current.OnBuildingInfoMenu(currentBuilding);
    }

    #region Event Functions
    public void OpenUI(ProductionBuilding building)
    {
        MyUI.SetActive(true);
        PopulateArea(building.RecipeBook);
        currentBuilding = building;
    }

    public void CloseUI()
    {
        ClearObjects();
        MyUI.SetActive(false);
        currentBuilding = null;
    }
    #endregion

    void PopulateArea(List<Recipe> RecipieBook)
    {
        int startX = -550;
        int buff = 350;
        int startY = 350;

        int countRow = 4;

        Vector3 StartPos = new Vector3(startX, startY, 0.0f);
        for(int i = 0; i < RecipieBook.Count; ++i)
        {
                //int index = i * countRow + j;
            var go = Instantiate(Icon) as GameObject;
            go.name = RecipieBook[i].name;
            go.transform.SetParent(Parent.transform);
            var trans = go.GetComponent<RectTransform>();
            var item = go.AddComponent<UI_Item_ProductionBuilding>();
            trans.localScale = new Vector3(1f, 1f, 1f);
            trans.localPosition = new Vector3(startX + (i%countRow) * buff, 
                startY + (i / countRow) * -buff, -1f);

            item.Set(RecipieBook[i], this);

            objects.Add(go);
        }
    }

    public void OnRecipieSelect(Item i)
    {
        //set the recipie on building
        currentBuilding.SetRecipie(i);
        EventManager.current.OnCloseMenue();
    }

    void ClearObjects()
    {
        for(int i = 0; i < objects.Count; ++i)
        {
            Destroy(objects[i].gameObject);
        }

        objects.Clear();
    }

}
