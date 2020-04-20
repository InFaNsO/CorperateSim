using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetClipingButton : MonoBehaviour
{

    [SerializeField] Transform UIElementTransform = null;

    Vector3 offset;

    private void OnValidate()
    {
        if (UIElementTransform)
        {
            SetOffset();
            UpdateButtons();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetOffset();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtons();
    }

    void UpdateButtons()
    {
        Vector3 toScreen = Camera.main.WorldToScreenPoint(transform.position);// + offset);
        UIElementTransform.position = toScreen;
    }

    void SetOffset()
    {
       // offset = Camera.main.ScreenToWorldPoint(UIElementTransform.position) - transform.position;
    }

    public void Enable()
    {
        UIElementTransform.gameObject.SetActive(true);
    }
    public void Disable()
    {
        UIElementTransform.gameObject.SetActive(false);
    }
}
