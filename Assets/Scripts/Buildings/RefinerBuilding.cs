using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinerBuilding : MonoBehaviour
{
    public Recipe recipe = null;
    [SerializeField] public List<Recipe> RecipeBook;
    Item OutPutSlot;
    Item InputSlot;
    [SerializeField] uint currentQ = 0;

    [SerializeField] BuilderManager buildManager;
    [SerializeField] ConveyorInputSlot input;
    [SerializeField] ConveyorOutputSlot output;

    // Start is called before the first frame update
    void Start()
    {
        //ForTEsting
        output.bm = buildManager;
        input.bm = buildManager;
        SetRecipie(RecipeBook[0].finalProduct.item);
    }

    // Update is called once per frame
    void Update()
    {
        if (!recipe)
            return;

        if(input.belt)
        {
            //GetShit
        }


        OutPutSlot.currentQuantity += recipe.Produce(InputSlot);
        OutPutSlot.currentQuantity = (uint)Mathf.Min(OutPutSlot.maxQuantity, OutPutSlot.currentQuantity);
        currentQ = OutPutSlot.currentQuantity;

        if(output.belt)
        {
            //Send shit
        }
    }

    public void SetRecipie(Item finalProduct)
    {
        for(int i = 0; i < RecipeBook.Count; ++i)
        {
            if(RecipeBook[i].finalProduct.item == finalProduct)
            {
                recipe = Instantiate(RecipeBook[i]);
                //recipe.Clone(RecipeBook[i]);
                InputSlot = Instantiate(recipe.Ingredients[0].item);
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
        InputSlot.currentQuantity = InputSlot.maxQuantity;
    }
}
