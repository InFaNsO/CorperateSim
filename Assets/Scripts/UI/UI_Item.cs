using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    [SerializeField] public Image myImage;
    [SerializeField] public TMP_Text myText;
    [SerializeField] public Item myItem;
    [SerializeField] public ButtonManagerBasic myButton;

    public void Logger()
    {
        Debug.Log("Button Pressed");
    }
}
