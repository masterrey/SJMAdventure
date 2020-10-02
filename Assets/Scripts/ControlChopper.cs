using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChopper : MonoBehaviour
{
    public Transform rotor;
    public Transform tailrotor;
    internal bool onBoard;
    float motorrpm = 0;
    float motorrpmdesired = 0;
    private Vector3 movplayer;
    public Rigidbody rdb;
    public float liftPower=10000;
    public float torquePower=1000;
    public AudioSource motorblades;
    // Start is called before the first frame update
    void Start()
    {

        rdb.centerOfMass -= new Vector3(0, 0.0f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        rotor.Rotate(Vector3.forward *  motorrpm*59);
        tailrotor.Rotate(Vector3.right * motorrpm*59);

        if (onBoard)
            movplayer = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Coletive"), Input.GetAxis("Vertical"));


        motorrpm = Mathf.Lerp(motorrpm, motorrpmdesired, Time.deltaTime/10);
        motorblades.pitch = motorrpm*1.5f;
        //motorblades.volume = motorrpm;

        if (onBoard)
        {
            motorrpmdesired = 1;
         
            
        }
        else
        {
            motorrpmdesired = 0;
        }
    }

    void FixedUpdate()
    {
        //pairar
        rdb.AddRelativeForce(Vector3.up * motorrpm * 19000);
        //voar
        rdb.AddRelativeForce(Vector3.up * movplayer.y* liftPower);

        rdb.AddRelativeTorque(Vector3.right * movplayer.z * torquePower);

        rdb.AddRelativeTorque(Vector3.up * movplayer.x * torquePower);

        float contratorque = Vector3.Dot(transform.right, Vector3.up);
        

       rdb.AddRelativeTorque(Vector3.forward * -contratorque * torquePower);

    }
}
