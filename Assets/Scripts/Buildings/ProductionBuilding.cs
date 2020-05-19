using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ProductionBuilding : MonoBehaviour
{
    public Recipe recipe = null;
    [SerializeField] public List<Recipe> RecipeBook;
    Item OutPutSlot;
    List<Item> InputSlots = new List<Item>();
    [SerializeField] uint currentQ = 0;

    [SerializeField] BuilderManager builderManager;
    [SerializeField] List<ConveyorInputSlot> inputs = new List<ConveyorInputSlot>();
    [SerializeField] ConveyorOutputSlot outPut = null;

    [SerializeField] GameObject resourceObject;
    GameObject ro = null;

    bool hasPower = false;

    List<GameObject> inCommingResourcesToBeDestroyed = new List<GameObject>();
    [SerializeField] Transform resourceSpawnPoint = null;

    void Start()
    {
        //ForTEsting
        SetRecipie(RecipeBook[0].finalProduct.item);
    }

    void Update()
    {
        if (!recipe)
            return;

        if(!hasPower)
        {
            //getPower
        }

        for (int i = 0; i < inputs.Count; ++i)
        {
            if (inputs[i].belt)
            {
                //Get shit
            }
        }

        OutPutSlot.currentQuantity += recipe.Produce(InputSlots);
        OutPutSlot.currentQuantity = (uint)Mathf.Min(OutPutSlot.maxQuantity, OutPutSlot.currentQuantity);
        currentQ = OutPutSlot.currentQuantity;

        if (outPut.belt && OutPutSlot.currentQuantity > 0)
        {
            //check if the initial position is free then add
            //assuming its free
            var go = Instantiate(ro, resourceSpawnPoint.position, resourceSpawnPoint.rotation);
            var r = go.GetComponent<Resource>();
            r.myCurrentBelt = outPut.belt;
            r.SetPath();
            OutPutSlot.currentQuantity -= 1;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < inCommingResourcesToBeDestroyed.Count; ++i)
            Destroy(inCommingResourcesToBeDestroyed[i]);
        inCommingResourcesToBeDestroyed.Clear();
    }

    public void SetRecipie(Item finalProduct)
    {
        for (int i = 0; i < RecipeBook.Count; ++i)
        {
            if (RecipeBook[i].finalProduct.item == finalProduct)
            {
                recipe = Instantiate(RecipeBook[i]);
                for (int j = 0; j < recipe.Ingredients.Count; ++j)
                {
                    InputSlots.Add(Instantiate(recipe.Ingredients[j].item));
                }

                //recipe.Clone(RecipeBook[i]);
                OutPutSlot = Instantiate(recipe.finalProduct.item);
                if (OutPutSlot.currentQuantity > 0)
                    OutPutSlot.currentQuantity = 0;
                //forTesting();
                break;
            }
        }
        SetResourceObject();
    }

    void SetResourceObject()
    {
        if (ro)
            Destroy(ro);
        ro = Instantiate(resourceObject, transform.position, transform.rotation);
        ro.transform.position += Vector3.up * 3.0f;
        ro.GetComponent<Resource>().item = recipe.finalProduct.item;
        ro.GetComponent<MeshFilter>().sharedMesh = recipe.finalProduct.item.model;
        ro.GetComponent<MeshRenderer>().material = recipe.finalProduct.item.mat;
    }

    public void ShutDown()
    {
        hasPower = false;
    }
    public void TurnOn()
    {
        hasPower = true;
    }

    public void AddResource(GameObject resource)
    {
        var r = resource.GetComponent<Resource>();
        for(int i = 0; i < recipe.Ingredients.Count; ++i)
        {
            if(recipe.Ingredients[i].item == r.item)
            {
                InputSlots[i].currentQuantity += 1;
                inCommingResourcesToBeDestroyed.Add(resource);
                break;
            }
        }
    }
}
