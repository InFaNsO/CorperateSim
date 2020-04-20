using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenue : MonoBehaviour
{
    [SerializeField] int MainMenueBuildIndex = 0;

    public void GoToMainMenue()
    {
        SceneManager.LoadScene(MainMenueBuildIndex);
    }

    private void Update()
    {
        
    }
}
