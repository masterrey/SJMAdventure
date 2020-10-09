using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnChopter : GetOnBoard
{
    [SerializeField]
    ControlChopper controlChopper;

    public override void CallVehicleControl(bool onboard)
    {
        if (controlChopper)
        {
            controlChopper.onBoard = onboard;
            controlChopper.gameObject.transform.parent = null;
            DontDestroyOnLoad(controlChopper.gameObject);
        }
    }

    IEnumerator OpenDoor()
    {
        bool wait = true;
        float angDoor = 0;
        while (wait)
        {
            yield return new WaitForFixedUpdate();
            angDoor += Time.fixedDeltaTime * 120;
            //door.transform.localRotation = Quaternion.Euler(0, angDoor, 0);
            door.transform.localPosition = new Vector3(0, 0, angDoor / 30);
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
            //door.transform.localRotation = Quaternion.Euler(0, angDoor, 0);
            door.transform.localPosition = new Vector3(0, 0, angDoor / 30);
            if (angDoor <= 0.1f)
            {
                wait = false;
            }
        }
        door.transform.localPosition = new Vector3(0, 0,0);
        //door.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
