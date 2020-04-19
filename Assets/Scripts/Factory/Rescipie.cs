using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescipie : MonoBehaviour
{
    public Resource FinalProduct;
    public float ResourcePerMin = 60.0f;
    public List<Ingredient> Ingredients;

    public GameObject resourcePrefab;
}
