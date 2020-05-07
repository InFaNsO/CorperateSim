using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUIManager : MonoBehaviour
{
    [SerializeField] GameObject BuildMenue;

    // Start is called before the first frame update
    void Start()
    {
        BuildMenue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BuildMenue.SetActive(false);
        if(Input.GetKeyDown(KeyCode.B))
        {
            BuildMenue.SetActive(true);
        }
    }

    public void DeactivateBuild()
    {
        BuildMenue.SetActive(false);
    }
}
