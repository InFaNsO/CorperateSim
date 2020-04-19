using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMovementManagerPlayer : MonoBehaviour
{
    Transform parentTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition != Vector3.zero)
        {
            //move it back to 0 and move parent
            parentTransform.Translate(transform.localPosition);
            transform.Translate(-transform.localPosition);
        }
        if(transform.localRotation != Quaternion.identity)
        {
            //apply this rotation to parent
            transform.localRotation = Quaternion.identity;
            //Vector3 localAxis= new Vector3();
            //float localAngle = 0.0f;
            //transform.localRotation.ToAngleAxis(out localAngle, out localAxis);
            //parentTransform.Rotate(localAxis, localAngle);
            //transform.Rotate(localAxis, -localAngle);
        }
    }
}
