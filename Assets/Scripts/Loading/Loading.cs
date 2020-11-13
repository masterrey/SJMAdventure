using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    static string levelToLoad;
    AsyncOperation asyncOperation;
    public TextMesh loadingprogress;
    float loadingnprogress = 0;
    public static void MyLoading(string myleveltoload)
    {
        levelToLoad = myleveltoload;
        SceneManager.LoadScene("loading");
    }


    // Start is called before the first frame update
    void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync(levelToLoad);
        asyncOperation.allowSceneActivation = false;

    }

    // Update is called once per frame
    void Update()
    {

        loadingnprogress = Mathf.Lerp(loadingnprogress,asyncOperation.progress + .1f,Time.deltaTime*10);

        loadingprogress.text = (loadingnprogress).ToString("00%");

        
        if (Input.anyKey && loadingnprogress > 0.9f)
        {
            asyncOperation.allowSceneActivation = true;
        }
    }
}
