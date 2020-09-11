using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCar : MonoBehaviour
{
    public WheelCollider rodaDE;
    public WheelCollider rodaDD;
    public WheelCollider rodaTE;
    public WheelCollider rodaTD;
    private Vector3 movplayer;
    public float motorTorque = 100;
    Rigidbody rdb;
    public bool onBoard = false;
    // Start is called before the first frame update
    void Start()
    {
        rdb.GetComponent<Rigidbody>();
        rdb.centerOfMass = rdb.centerOfMass - new Vector3(0, -0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(onBoard)
        movplayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    }
    private void FixedUpdate()
    {
        rodaTD.motorTorque = movplayer.z* motorTorque;
        rodaTE.motorTorque = movplayer.z* motorTorque;

        rodaDE.steerAngle = movplayer.x * 30;
        rodaDD.steerAngle = movplayer.x * 30;
    }
}
