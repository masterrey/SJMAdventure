﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoard : MonoBehaviour
{
    public GameObject door;
    ControlPlayer controlPlayer;
    public ControlCar controlCar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TryToBoard()
    {
        print("trying to board");
        controlPlayer.gameObject.transform.parent = transform;
        controlPlayer.gameObject.transform.localPosition = Vector3.zero;
        controlPlayer.gameObject.gameObject.SetActive(false);
        controlCar.onBoard = true;
        door.transform.localRotation = Quaternion.Euler(0, 0, 0);
        controlPlayer.wantToEnter -= TryToBoard;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.transform.localRotation =Quaternion.Euler(0, 60, 0);
            controlPlayer = other.gameObject.GetComponentInParent<ControlPlayer>();
            controlPlayer.wantToEnter += TryToBoard;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (controlPlayer)
        {
            controlPlayer.wantToEnter -= TryToBoard;
            door.transform.localRotation = Quaternion.Euler(0, 0, 0);
            controlPlayer = null;
        }
    }
}