using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCCity : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] waylist;
    public Animator anim;
    public Vector3 destination;
    int coroutinesstarted=0;
    public enum States 
    {
        Idle,
        Patrol,
        Runaway
    }
    public States state ;

    // Start is called before the first frame update
    void Start()
    {
      
        waylist =GameObject.FindGameObjectsWithTag("Waypoint");


        ForcedChangeState(States.Idle);
    }

    void ChangeState(States current)
    {
        if (current != state)
        {
            print("change " + current.ToString());
            anim.SetInteger("States", (int)current);
            StartCoroutine(current.ToString());
        }
    }

    void ForcedChangeState(States current)
    {
            anim.SetInteger("States", (int)current);
            StartCoroutine(current.ToString());
    }

    IEnumerator Idle()
    {
        state = States.Idle;
        coroutinesstarted++;
        agent.isStopped = true;
        float timetowait = Random.Range(1, 5);
        yield return new WaitForSeconds(timetowait);
        
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
            if (Vector3.Distance(destination,transform.position)<2)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        ChangeState(States.Idle);
        coroutinesstarted--;
    }

}
