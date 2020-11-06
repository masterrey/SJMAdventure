using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public GameObject meshWheel;
    
    void Start()
    {
        if(!wheelCollider)
        wheelCollider = GetComponent<WheelCollider>();
        if (!meshWheel)
            meshWheel = transform.GetChild(0).gameObject;
        
    }

   
    void FixedUpdate()
    {
       
        wheelCollider.GetWorldPose(out Vector3 wposition,out Quaternion wrotation);
        meshWheel.transform.position = wposition;
        meshWheel.transform.rotation = wrotation;

    }
}
