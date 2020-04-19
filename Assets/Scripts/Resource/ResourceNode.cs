using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] public ResourceTypes mType;
    [SerializeField] public ResourceQuality mQuality;

    [SerializeField] private float ResourcePerMin = 60;
    [SerializeField] private int MaxCapacity = 10;

    private float mCurrentCapacity = 0.0f;

    float nextResourceTime = 0;
    bool mined = false;
    float TimeFor1;

    [SerializeField] GameObject myResource;

    UnityEngine.XR.WSA.WorldManager myWorld;

    public void Start()
    {
        //Randomize();
        TimeFor1 = 1.0f / ResourcePerMin;
    }

    public void Update()
    {
        if(mCurrentCapacity <= MaxCapacity)
            Produce();

        //check for other variables such as electricity
    }


    public void Randomize()
    {
        mType = (ResourceTypes)Random.Range(0, (int)ResourceTypes.count);
        mQuality = (ResourceQuality)Random.Range(0, 3);

        ResourcePerMin /= (int)mType;


        myResource = GameObject.Find("Resource" + mType.ToString());
    }

    void Produce()
    {
        if (Time.time < nextResourceTime)
            return;

        nextResourceTime = Time.time + TimeFor1;
        mCurrentCapacity += 1.0f;
    }

    public float GetResource(float maxCapacity)
    {
        float returnval;
        if(mCurrentCapacity > maxCapacity)
        {
            returnval = maxCapacity;
            mCurrentCapacity -= maxCapacity;
        }
        else
        {
            returnval = mCurrentCapacity;
            mCurrentCapacity = 0.0f;
        }
        return returnval;
    }

    public GameObject GetResourceObject()
    {
        return myResource;
    }
}


