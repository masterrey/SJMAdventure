using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoard : MonoBehaviour
{
    public GameObject door;
    ControlPlayer controlPlayer;
    [SerializeField]
    internal ControlCar controlCar;
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
        controlPlayer.gameObject.transform.localRotation = Quaternion.identity;
        // controlPlayer.gameObject.gameObject.SetActive(false);
        CallVehicleControl(true);
        StartCoroutine("CloseDoor");
        controlPlayer.wantToEnter -= TryToBoard;
        SterringWheel = sterringWheel;
        controlPlayer.wantToExit += TryToOffBoard;
        
        return true;
    }

    public bool TryToOffBoard(out GameObject SterringWheel)
    {
        print("trying to Offboard");
        controlPlayer.gameObject.transform.parent = null;
        controlPlayer.gameObject.transform.position = transform.position + Vector3.left * 2;
        controlPlayer.gameObject.transform.localRotation = Quaternion.identity;
        CallVehicleControl(false);
        StartCoroutine("OpenDoor");
        controlPlayer.wantToExit -= TryToBoard;
        SterringWheel = null;

        return true;
    }

    public virtual void CallVehicleControl(bool onboard)
    {
        if (controlCar)
        {
            controlCar.onBoard = onboard;
            controlCar.gameObject.transform.parent = null;
            DontDestroyOnLoad(controlCar.gameObject);
        }
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
            //controlPlayer = null;
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
