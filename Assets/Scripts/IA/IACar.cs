using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACar : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject[] waylist;
    private Vector3 movia;
    public Vector3 destination;
    int coroutinesstarted = 0;
    public WheelCollider rodaDE;
    public WheelCollider rodaDD;
    public WheelCollider rodaTE;
    public WheelCollider rodaTD;
    public float motorTorque = 100;
    public enum States
    {
        Idle,
        Patrol,
        Runaway
    }
    public States state;

    // Start is called before the first frame update
    void Start()
    {
        waylist = GameObject.FindGameObjectsWithTag("WayCar");
        ForcedChangeState(States.Idle);
    }

    void ChangeState(States current)
    {
        if (current != state)
        {
            print("change " + current.ToString());
           
            StartCoroutine(current.ToString());
        }
    }

    void ForcedChangeState(States current)
    {
      
        StartCoroutine(current.ToString());
    }

    IEnumerator Idle()
    {
        state = States.Idle;
        coroutinesstarted++;
        agent.isStopped = true;
        float timetowait = Random.Range(1, 5);
        yield return new WaitForSeconds(timetowait);
        movia = Vector3.zero;
        ChangeState(States.Patrol);
        coroutinesstarted--;
    }

    IEnumerator Patrol()
    {
        state = States.Patrol;
        coroutinesstarted++;
        agent.isStopped = false;
        destination = waylist[Random.Range(0, waylist.Length)].transform.position;
        agent.SetDestination(destination);
        
        
        while (state == States.Patrol)
        {
            agent.transform.localPosition = Vector3.zero;
            agent.transform.localRotation = Quaternion.identity;
            Vector3 dirtogo = agent.steeringTarget - transform.position;
            float ang = Vector3.SignedAngle(dirtogo.normalized, transform.forward,Vector3.down);
            //print(ang);
            ang = Mathf.Clamp(ang, -30, 30);
            movia = new Vector3(ang, 0,1);
            
            if (Vector3.Distance(destination, transform.position) < 5)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        ChangeState(States.Idle);
        coroutinesstarted--;
    }

    private void FixedUpdate()
    {
        rodaTD.motorTorque = movia.z * motorTorque;
        rodaTE.motorTorque = movia.z * motorTorque;

        rodaDE.steerAngle = movia.x;
        rodaDD.steerAngle = movia.x;
    }
}
