using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractorBuilding : MonoBehaviour
{
    public Recipe recipe = null;
    [SerializeField] public List<Recipe> RecipeBook;
    Item OutPutSlot;
    [SerializeField] uint currentQ = 0;

    [SerializeField] BuilderManager buildManager;
    [SerializeField] ConveyorOutputSlot output;

    //node collider will set the Reciepie & 

    // Start is called before the first frame update
    private void Awake()
    {
        output.bm = buildManager;
    }

    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (!recipe)
            return;

        OutPutSlot.currentQuantity += recipe.Produce();
        OutPutSlot.currentQuantity = (uint)Mathf.Min(OutPutSlot.maxQuantity, OutPutSlot.currentQuantity);
        currentQ = OutPutSlot.currentQuantity;

        if(output.belt)
        {
            //send shit through;
        }
    }

    public void NodeResourceSetup(ResourceNode node)
    {
        for(int i = 0; i < RecipeBook.Count; ++i)
        {
            if (RecipeBook[i].finalProduct.item == node.mType)
            {
                recipe = Instantiate(RecipeBook[i]);
                recipe.Awake();
                OutPutSlot = Instantiate(node.mType);
                return;
            }
        }
    }
}
