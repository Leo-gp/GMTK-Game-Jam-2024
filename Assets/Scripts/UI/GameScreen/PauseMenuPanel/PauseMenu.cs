using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (gameManager != null)
        {
            gameManager.OnPauseKeyPress += OnPauseKeyPressHandler;
        }

        canvasGroup = GetComponent<CanvasGroup>();
        DeactivatePauseMenu();
    }

    private void OnPauseKeyPressHandler(GameManager gameManager)
    {
        Action action = gameManager.IsPaused ? ActivatePauseMenu : DeactivatePauseMenu;
        action();
    }

    public void DeactivatePauseMenu()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ActivatePauseMenu()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void OnDisable()
    {
        gameManager.OnPauseKeyPress -= OnPauseKeyPressHandler;
    }
}
