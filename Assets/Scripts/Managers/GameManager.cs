using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<GameManager> OnPauseKeyPress;

    [SerializeField] private KeyCode shortKey;

    [SerializeField] public bool IsPaused { get; private set; } = false;


    private void Update()
    {
        if (Input.GetKeyDown(shortKey))
        {
            TogglePause();
            OnPauseKeyPress?.Invoke(this);
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
    }
}
