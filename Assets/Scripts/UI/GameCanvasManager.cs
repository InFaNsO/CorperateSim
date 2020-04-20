using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenue;

    GameObject currentlyActiveMenue = null;

    private void OnValidate()
    {
        GetInnerComponents();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetInnerComponents();

        //Set All Menues unactive
        PauseMenue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //for pause menue
        CheckMenue(KeyCode.Escape, PauseMenue);
    }

    void CheckMenue(KeyCode key, GameObject menue)
    {
        if (Input.GetKeyDown(key))
        {
            menue.SetActive(true);
            if (currentlyActiveMenue)
                currentlyActiveMenue.SetActive(false);
            currentlyActiveMenue = menue;
        }
    }

    void GetInnerComponents()
    {

    }
}
