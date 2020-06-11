using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/LinearProgressBar"));
        if(Selection.activeGameObject)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RadialProgressBar"));
        if(Selection.activeGameObject)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    public int Minimum;
    public int Maximum;
    public int Current;

    public Image Mask;
    public Image Fill;

    public Image OutlineBG;
    public Image OutlineBar;

    public Color BarColor;
    public Color OutlineColor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentFill();
    }

    void SetCurrentFill()
    {
        float currentOffset = Current - Minimum;
        float maxOffset = Maximum - Minimum;
        float fillAmt = currentOffset / maxOffset;

        Mask.fillAmount = fillAmt;
        Fill.color = BarColor;

        OutlineBar.color = OutlineColor;
        OutlineBG.color = OutlineColor;
    }
}
