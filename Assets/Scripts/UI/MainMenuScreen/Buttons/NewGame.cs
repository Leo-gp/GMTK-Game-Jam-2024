using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{    
    private const string GAME_SCENE = "Game";

    public void OnClickHandle()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }
}
