using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    [SerializeField] private MainMenuScreen MainMenuScreenCanvasGroups;
    //[SerializeField] private float fadingSpeed = 5f;
    private const string GAME_SCENE = "Game";

    public void OnClickHandle()
    {
        /* In case it needs some fade out
        float currentAlpha = MainMenuScreenCanvasGroups.GetComponent<CanvasGroup>().alpha;
        float fadingAlpha = Mathf.Lerp(currentAlpha, 0f, fadingSpeed);

        MainMenuScreenCanvasGroups.SetMainMenuScreenAlpha(fadingAlpha);
        */

        SceneManager.LoadScene(GAME_SCENE);
    }
}
