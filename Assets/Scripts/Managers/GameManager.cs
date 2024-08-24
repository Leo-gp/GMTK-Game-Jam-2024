using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private KeyCode shortKey;

    public bool IsPaused { get; private set; }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(shortKey))
        {
            TogglePause();
            OnPauseKeyPress?.Invoke(this);
        }
    }

    public event Action<GameManager> OnPauseKeyPress;

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
    }
}