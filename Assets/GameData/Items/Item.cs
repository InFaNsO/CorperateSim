 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ProductionMachine
{
    Extractor,
    Refiner,
    Convertor,
    Factory1,
    Factory2,
    Factory3
}


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public ProductionMachine producedBy = ProductionMachine.Factory1;

    public uint currentQuantity = 0;
    public uint maxQuantity = 50;

    public Sprite icon;
    public Mesh model;
}
