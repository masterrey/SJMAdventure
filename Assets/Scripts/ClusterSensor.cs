using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClusterSensor : MonoBehaviour
{
    public string scenename;
    public GameObject player;
    bool loaded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 300 && !loaded)
        {
            SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);
            loaded = true;
        }

        if (Vector3.Distance(player.transform.position, transform.position) > 300 && loaded)
        {
            SceneManager.UnloadSceneAsync(scenename);
            loaded = false;
        }

    }
}
