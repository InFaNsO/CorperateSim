using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public float GravitationalConstant = 0.5f;
    public List<PlanetSystem> ObjectsInSystem = new List<PlanetSystem>();

    //debugging
    float deltaTime = 1.0f; 
    int numberOfSteps = 10;
    List<Vector3> currentPosForPlanets = new List<Vector3>();
    List<Vector3> previousPosForPlanets = new List<Vector3>();

    private void OnValidate()
    {
        currentPosForPlanets.Clear();
        previousPosForPlanets.Clear();
        for (int i = 0; i < ObjectsInSystem.Count; ++i)
        {
            currentPosForPlanets.Add(ObjectsInSystem[i].transform.position);
           previousPosForPlanets.Add(ObjectsInSystem[i].transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < ObjectsInSystem.Count; ++i)
        //{
        //    currentPosForPlanets.Add(ObjectsInSystem[i].transform.position);
        //    previousPosForPlanets.Add(ObjectsInSystem[i].transform.position);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
    }

    Vector3 GetUpdatedPos(Vector3 currentPos, Vector3 currentVel, PlanetSystem planet, float deltaTime)
    {
        for (int i = 0; i < ObjectsInSystem.Count; ++i)
        {
            if (planet == ObjectsInSystem[i])
                continue;

            bool ignore = false;
            for (int j = 0; j < planet.ignoreThem.Count; ++j)
            {
                if (ObjectsInSystem[i] == planet.ignoreThem[j])
                {
                    ignore = true;
                    break;
                }
            }
            if (ignore)
                continue;

            Vector3 dir = ObjectsInSystem[i].transform.position - planet.transform.position;
            Vector3 acceleration = dir.normalized * GravitationalConstant * ObjectsInSystem[i].Mass / dir.sqrMagnitude;
            currentVel += acceleration * Time.deltaTime;
        }

        Vector3 pos = currentPos + (currentVel * deltaTime);

        return pos;
    }

    private void OnDrawGizmos()
    {
        //get the currentPositions
        for (int i = 0; i < ObjectsInSystem.Count; ++i)
        {
            currentPosForPlanets[i] = ObjectsInSystem[i].transform.position;
            previousPosForPlanets[i] = ObjectsInSystem[i].transform.position;
        }
        Vector3 pos1 = currentPosForPlanets[1];

        for (int i = 1; i < ObjectsInSystem.Count; ++i)
        {
            for (int j = 0; j < numberOfSteps; ++j)
            {
                currentPosForPlanets[i] = GetUpdatedPos(previousPosForPlanets[i], ObjectsInSystem[i].GetCurrentVelocity(), ObjectsInSystem[i], deltaTime);
                Gizmos.color = ObjectsInSystem[i].PlanetPathColor;
                Gizmos.DrawLine(previousPosForPlanets[i], currentPosForPlanets[i]);
                Gizmos.DrawSphere(currentPosForPlanets[i], 10.0f);
                previousPosForPlanets[i] = currentPosForPlanets[i]; 
            }
        }

        Gizmos.DrawLine(pos1, currentPosForPlanets[1]);
    }
}
