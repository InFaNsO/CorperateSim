using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public Recipe recipe = null;
    [SerializeField] public List<Recipe> RecipeBook;
    Item OutPutSlot;
    List<Item> InputSlots = new List<Item>();
    [SerializeField] uint currentQ = 0;

    [SerializeField] BuilderManager builderManager;
    [SerializeField] List<ConveyorInputSlot> inputs;
    [SerializeField] ConveyorOutputSlot outPut;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < inputs.Count; ++i)
            inputs[i].bm = builderManager;
        outPut.bm = builderManager;

        //ForTEsting
        SetRecipie(RecipeBook[0].finalProduct.item);
    }

    // Update is called once per frame
    void Update()
    {
        if (!recipe)
            return;

        for(int i = 0; i < inputs.Count; ++i)
        {
            if(inputs[i].belt)
            {
                //Get shit
            }
        }

        OutPutSlot.currentQuantity += recipe.Produce(InputSlots);
        OutPutSlot.currentQuantity = (uint)Mathf.Min(OutPutSlot.maxQuantity, OutPutSlot.currentQuantity);
        currentQ = OutPutSlot.currentQuantity;

        if(outPut.belt)
        {
            //Send Shit
        }
    }

    public void SetRecipie(Item finalProduct)
    {
        for (int i = 0; i < RecipeBook.Count; ++i)
        {
            if (RecipeBook[i].finalProduct.item == finalProduct)
            {
                recipe = Instantiate(RecipeBook[i]);
                for(int j = 0; j < recipe.Ingredients.Count; ++j)
                {
                    InputSlots.Add(Instantiate(recipe.Ingredients[j].item));
                }

                //recipe.Clone(RecipeBook[i]);
                //InputSlot = Instantiate(recipe.Ingredients[0].item);
                OutPutSlot = Instantiate(recipe.finalProduct.item);
                if (OutPutSlot.currentQuantity > 0)
                    OutPutSlot.currentQuantity = 0;
                forTesting();
                return;
            }
        }
    }
    void forTesting()
    {
        for (int j = 0; j < recipe.Ingredients.Count; ++j)
            InputSlots[j].currentQuantity = InputSlots[j].maxQuantity;
    }
}
