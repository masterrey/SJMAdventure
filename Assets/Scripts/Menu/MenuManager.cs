using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    #region Unity Padrao
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion

    #region botoes do jogo
    public void _StartGame()
    {
        Loading.MyLoading("MainScene");

        //SceneManager.LoadScene("MainScene");
    }
    public void _Options()
    {
        print("Options");
    }
    public void _Quit()
    {
        print("Quitting..");
        Application.Quit();
    }
    #endregion
}
