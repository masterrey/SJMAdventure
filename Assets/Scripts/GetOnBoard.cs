using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoard : MonoBehaviour
{
    public GameObject door;
    ControlPlayer controlPlayer;
    public ControlCar controlCar;
    public GameObject seat;
    public GameObject sterringWheel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool TryToBoard(out GameObject SterringWheel)
    {
        print("trying to board");
        controlPlayer.gameObject.transform.parent = seat.transform;
        controlPlayer.gameObject.transform.localPosition = Vector3.zero;
       // controlPlayer.gameObject.gameObject.SetActive(false);
        controlCar.onBoard = true;
        StartCoroutine("CloseDoor");
        controlPlayer.wantToEnter -= TryToBoard;
        SterringWheel = sterringWheel;
        return true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("OpenDoor");
            controlPlayer = other.gameObject.GetComponentInParent<ControlPlayer>();
            controlPlayer.wantToEnter += TryToBoard;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (controlPlayer)
        {
            controlPlayer.wantToEnter -= TryToBoard;
            StartCoroutine("CloseDoor");
            controlPlayer = null;
        }
    }

    IEnumerator OpenDoor()
    {
        bool wait=true;
        float angDoor = 0;
        while (wait)
        {
            yield return new WaitForFixedUpdate();
            angDoor += Time.fixedDeltaTime*120;
            door.transform.localRotation = Quaternion.Euler(0, angDoor, 0);
            if (angDoor >= 60)
            {
                wait = false;
            }
        }
    }

    IEnumerator CloseDoor()
    {
        bool wait = true;
        float angDoor = 60;
        while (wait)
        {
            yield return new WaitForFixedUpdate();
            angDoor -= Time.fixedDeltaTime * 240;
            door.transform.localRotation = Quaternion.Euler(0, angDoor, 0);
            if (angDoor <= 0.1f)
            {
                wait = false;
            }
        }
        door.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
