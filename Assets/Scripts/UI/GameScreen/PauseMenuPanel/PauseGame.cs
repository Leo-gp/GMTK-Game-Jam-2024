using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool IsPaused {  get; private set; }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        IsPaused = false;

    }
}
