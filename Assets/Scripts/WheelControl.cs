using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public GameObject meshWheel;
    
    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
        
    }

   
    void FixedUpdate()
    {
       
        wheelCollider.GetWorldPose(out Vector3 wposition,out Quaternion wrotation);
        meshWheel.transform.position = wposition;
        meshWheel.transform.rotation = wrotation;

    }
}
