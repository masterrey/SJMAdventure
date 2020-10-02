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

    public delegate bool WantTo(out GameObject obj);
    public WantTo wantToEnter;
    public WantTo wantToExit;
    private bool onboard=false;
    GameObject stwh;

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
        if (onboard)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                TryToExit();
            }
            return;
        }
        movplayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

       

        if (Input.GetButtonDown("Fire2"))
        {
            TryToEnter();
        }
    }

    void TryToEnter()
    {
        if (wantToEnter(out GameObject sterringWheel))
        {
            Collider[] cols = GetComponentsInChildren<Collider>();
            foreach (Collider col in cols)
            {
                col.enabled = false;
            }
            onboard = true;
            rdb.isKinematic = true;
            anim.SetBool("Driving", true);
            weapon.SetActive(false);
            stwh = sterringWheel;
        }
    }

    void TryToExit()
    {
        if (wantToExit(out GameObject sterringWheel))
        {
            Collider[] cols = GetComponentsInChildren<Collider>();
            foreach (Collider col in cols)
            {
                col.enabled = true;
            }

            onboard = false;
            rdb.isKinematic = false;
            anim.SetBool("Driving", false);
            weapon.SetActive(true);
            stwh = null;

        }

    }


        private void FixedUpdate()
    {
       
        weapon.transform.forward = aim.transform.forward;

        Vector3 locVelocity = transform.InverseTransformDirection(rdb.velocity); //transforma de mundo pra local
        Vector3 globMov = transform.TransformDirection(movplayer); //tranforma de local pra mundo

        globMov = new Vector3(globMov.x*3, rdb.velocity.y, globMov.z*3); //remove o y do movimento

        float ang = Vector3.Dot(transform.forward, -aim.transform.right);

        if (!onboard)
        {

            rdb.velocity = globMov; //modifica a velocidade da fisica

            

            transform.Rotate(transform.up, ang * 5);
        }

        aimspine.transform.rotation = aim.transform.rotation; //aplica a rotacao

       

        anim.SetFloat("Walk", locVelocity.z); //aplica o valor na animaçao
        anim.SetFloat("SideWalk", locVelocity.x+ ang*2);
    }

    private void OnAnimatorIK(int layerIndex)
    {
       
        anim.SetBoneLocalRotation(HumanBodyBones.Spine, aimspine.transform.localRotation);

        if (onboard && stwh)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, stwh.transform.position+stwh.transform.right*0.2f);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, stwh.transform.position - stwh.transform.right * 0.2f);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

            anim.SetIKRotation(AvatarIKGoal.RightHand, stwh.transform.rotation);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, stwh.transform.rotation);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        }
    }

}
