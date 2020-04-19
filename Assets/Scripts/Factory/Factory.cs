using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] public Rescipie rescipie;
    [HideInInspector] public List<float> inputResourceQuantity;

    public float MaxCapacity = 10;
    [HideInInspector] public float outputResourceQuantity;

    float nextTimeForResource;
    float TimeFor1;

    // Start is called before the first frame update
    void Start()
    {
        TimeFor1 = 1.0f / rescipie.ResourcePerMin;

        nextTimeForResource = Time.time + TimeFor1;

        for(int i = 0; i < rescipie.Ingredients.Count; ++i)
        {
            inputResourceQuantity.Add(0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inputResourceQuantity.Count; ++i)
        {
            if (inputResourceQuantity[i] < rescipie.Ingredients[i].Quantity)
                return;
        }

        //Add condition such as water and electricity
        Produce();
    }

    void Produce()
    {
        if (Time.time < nextTimeForResource)
            return;

        nextTimeForResource = Time.time + TimeFor1;
        outputResourceQuantity++;
    }

    public bool TakeResourceFromDrone(Drone d)
    {
        bool found = false;
        int i = 0;
        for (; i < rescipie.Ingredients.Count; ++i)
        {
            if(d.myCarryingResource.mType == rescipie.Ingredients[i].Type)
            {
                found = true;
                break;
            }
        }
        if (!found)
            return false;

        var ingre = rescipie.Ingredients[i];

        if (inputResourceQuantity[i] == ingre.capacity)
            return false;

        inputResourceQuantity[i] += d.mCurrentCapacity;
        d.mCurrentCapacity = 0.0f;

        if(inputResourceQuantity[i] >= ingre.capacity)
        {
            float diff = inputResourceQuantity[i] - ingre.capacity;
            inputResourceQuantity[i] = ingre.capacity;
            d.mCurrentCapacity = diff;
            return false;
        }

        //Desatroy the resource prefab 
        var r = d.GetComponentInChildren<Resource>();

        if (r)
        {
            var go = r.gameObject;
            GameObject.Destroy(go);
        }
        return true;
    }

    public bool GiveResourceToDrone(Drone d)
    {
        if (outputResourceQuantity < 1.0f)
            return false;

        d.mCurrentCapacity = outputResourceQuantity;
        if(d.mCurrentCapacity > d.MaxCarryCapacity)
        {
            outputResourceQuantity = d.mCurrentCapacity - d.MaxCarryCapacity;
            d.mCurrentCapacity = d.MaxCarryCapacity;
        }

        d.myCarryingResource = rescipie.FinalProduct;
        Instantiate(rescipie.resourcePrefab, d.transform);

        return true;
    }
}
