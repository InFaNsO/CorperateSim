using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetUIManager : MonoBehaviour
{
    [SerializeField] int PlanetSceneBuildIndex = 0;

    public void EnterPlanet()
    {
        SceneManager.LoadScene(PlanetSceneBuildIndex);
    }
}
