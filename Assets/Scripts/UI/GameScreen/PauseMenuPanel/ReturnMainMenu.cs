using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    private const string MAIN_MENU_SCENE = "MainMenu";

    public void OnClickHandle()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }
}
