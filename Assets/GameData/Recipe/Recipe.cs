using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public struct RecipeItem
{
    public Item item;
    public uint quantity;
}

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public List<RecipeItem> Ingredients;

    public uint ResourcesProducedPerMinute = 60;
    public ProductionMachine ProducedBy = ProductionMachine.Refiner;

    public RecipeItem finalProduct;

    public float ProduceNext = 0.0f;
    public uint TimeToProduce1 = 0;

    public void Awake()
    {
        TimeToProduce1 = 60 / ResourcesProducedPerMinute;
        ProduceNext = Time.time + TimeToProduce1;
    }

    uint Produce()
    {
        if (Time.time > ProduceNext)
        {
            float dt = Time.time - ProduceNext;
            uint mod = (uint)(dt / TimeToProduce1) + 1;
            ProduceNext = Time.time + TimeToProduce1;
            return finalProduct.quantity;
        }

        return 0;
    }

    public uint Produce(List<Item> items)
    {
        if (items.Count == 0)
            return Produce();

        List<uint> maxQ = new List<uint>();
        for(int i = 0; i < items.Count; ++i)
            maxQ.Add(items[i].currentQuantity / Ingredients[i].quantity);
        
        uint minMaxQ = uint.MaxValue;
        for(int i = 0; i < maxQ.Count; ++i)
            minMaxQ = (uint)Mathf.Min(minMaxQ, maxQ[i]);

        if (minMaxQ == 0)
            return 0;

        if (Time.time > ProduceNext)
        {
            float dt = Time.time - ProduceNext;
            uint mod = (uint)(dt / TimeToProduce1) + 1;
            uint quantity = (uint)Mathf.Min(mod, minMaxQ);
            ProduceNext = Time.time + TimeToProduce1;
            return finalProduct.quantity * quantity;
        }

        return 0;
    }

    public void Clone(Recipe r)
    {
        /*Ingredients = new List<RecipeItem>();
        for(int i = 0; i < r.Ingredients.Count; ++i)
        {
            Ingredients.Add(new RecipeItem());
            //Ingredients[i].item = Instantiate(r.Ingredients[i].item);
        }

        finalProduct = new RecipeItem();
        finalProduct.item = Instantiate(r.finalProduct.item);
        finalProduct.quantity = r.finalProduct.quantity;*/
    }
}
