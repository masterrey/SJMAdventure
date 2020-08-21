using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    Vector3 movplayer;
    Rigidbody rdb;
    public GameObject aim;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody[] rdbs = GetComponentsInChildren<Rigidbody>();
        Joint[] joints = GetComponentsInChildren<Joint>();
        foreach(Joint joint in joints)
        {
            Destroy(joint);
        }
        for(int i=1;i<rdbs.Length; i++)
        {
            Destroy(rdbs[i]);
        }
        rdb = rdbs[0];
        aim.transform.parent = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        movplayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    }


    private void FixedUpdate()
    {


        anim.SetFloat("Walk", rdb.velocity.magnitude);
        rdb.AddRelativeForce(movplayer * 1000);
        transform.rotation = Quaternion.Lerp(transform.rotation, aim.transform.rotation,Time.fixedDeltaTime);
    }
}
