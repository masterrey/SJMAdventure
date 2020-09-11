using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlPlayer : MonoBehaviour
{
    Vector3 movplayer;
    Rigidbody rdb;
    public GameObject aim;
    public GameObject aimspine;
    public Animator anim;

    public GameObject weapon;

    public delegate void WantToEnter();
    public WantToEnter wantToEnter;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody[] rdbs = GetComponentsInChildren<Rigidbody>();
        Joint[] joints = GetComponentsInChildren<Joint>();
        foreach (Joint joint in joints)
        {
            Destroy(joint);
        }
        for (int i = 1; i < rdbs.Length; i++)
        {
            Destroy(rdbs[i]);
        }
        rdb = rdbs[0];
        aim.transform.parent = Camera.main.transform;

        aim.transform.localPosition = Vector3.zero;
        aim.transform.localRotation = Quaternion.identity;
    }
    // Update is called once per frame
    void Update()
    {
        movplayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        weapon.transform.forward = aim.transform.forward;

        if (Input.GetButtonDown("Fire2"))
        {
            wantToEnter();
        }
    }


    private void FixedUpdate()
    {
        Vector3 locVelocity = transform.InverseTransformDirection(rdb.velocity); //transforma de mundo pra local
        Vector3 globMov = transform.TransformDirection(movplayer); //tranforma de local pra mundo

        globMov = new Vector3(globMov.x*3, rdb.velocity.y, globMov.z*3); //remove o y do movimento

       
       
        rdb.velocity = globMov ; //modifica a velocidade da fisica
        float ang = Vector3.Dot(transform.forward, -aim.transform.right);
        transform.Rotate(transform.up,ang*5);
        aimspine.transform.rotation = aim.transform.rotation; //aplica a rotacao

       

        anim.SetFloat("Walk", locVelocity.z); //aplica o valor na animaçao
        anim.SetFloat("SideWalk", locVelocity.x+ ang*2);
    }

    private void OnAnimatorIK(int layerIndex)
    {
       
        anim.SetBoneLocalRotation(HumanBodyBones.Spine, aimspine.transform.localRotation);
    }

}
