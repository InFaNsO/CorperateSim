using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetSystem : MonoBehaviour
{
    [SerializeField] public float Mass = 10.0f;
    [SerializeField] public float SurfaceGravity = -1f;
    [SerializeField] public float Radius = 10.0f;
    [SerializeField] public Vector3 initialVelocity = new Vector3();
                     Vector3 currentVelocity = new Vector3();

    [SerializeField] SolarSystem System = null;
    [SerializeField] public List<PlanetSystem> ignoreThem = new List<PlanetSystem>();

    PlanetClipingButton myUI;
    Collider myCollider;
    bool isSelected = false;

    //for debugging
    public Color PlanetPathColor;

    private void OnValidate()
    {
        //SetInnerVariables();

        if (SurfaceGravity > 0.0f && System)
        {
            Mass = SurfaceGravity * Radius * Radius / System.GravitationalConstant;
        }

        currentVelocity = initialVelocity;
        //transform.localScale = new Vector3(Radius, Radius, Radius);
    }

    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        SetInnerVariables();
        if (SurfaceGravity > 0.0f && System)
        {
            Mass = SurfaceGravity * Radius * Radius / System.GravitationalConstant;
        }

        currentVelocity = initialVelocity;
        //transform.localScale = new Vector3(Radius, Radius, Radius);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity();

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.collider == myCollider)
            {
                isSelected = true;
                myUI.Enable();
            }
            else
            {
                isSelected = false;
                myUI.Disable();
            }
        }
    }

    private void LateUpdate()
    {
        transform.position += currentVelocity * Time.deltaTime;
    }
    void UpdateVelocity()
    {
        for(int i = 0; i < System.ObjectsInSystem.Count; ++i)
        {
            if (this == System.ObjectsInSystem[i])
                continue;

            bool ignore = false;
            for(int j = 0; j < ignoreThem.Count; ++j)
            {
                if(System.ObjectsInSystem[i] == ignoreThem[j])
                {
                    ignore = true;
                    break;
                }
            }
            if (ignore)
                continue;

            Vector3 dir = System.ObjectsInSystem[i].transform.position - transform.position;
            Vector3 acceleration = dir.normalized * System.GravitationalConstant * System.ObjectsInSystem[i].Mass / dir.sqrMagnitude;
            currentVelocity += acceleration * Time.deltaTime;
        }
    }

    public Vector3 GetCurrentVelocity()
    {
        return currentVelocity;
    }


    void SetInnerVariables()
    {
        myUI = GetComponent<PlanetClipingButton>();
        myCollider = GetComponentInChildren<Collider>();
    }
}
