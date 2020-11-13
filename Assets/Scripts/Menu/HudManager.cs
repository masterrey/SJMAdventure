using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    static bool paused = false;
    public GameObject panelPause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static bool Paused()
    {
        return paused;
    }

    public void _ChangePauseState(bool pause)
    {
        panelPause.SetActive(pause);
        paused = pause;
        if (pause)
        {
            Time.timeScale = .0000001f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void _BackToMenu()
    {
        _ChangePauseState(false);
        
        Loading.MyLoading("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _ChangePauseState(!paused);
        }
       
    }
}
