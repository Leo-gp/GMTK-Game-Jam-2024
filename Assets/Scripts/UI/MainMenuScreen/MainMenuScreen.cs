using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetMainMenuScreenAlpha(float alpha)
    {
        alpha = Mathf.Clamp(alpha, 0, 1);
        canvasGroup.alpha = alpha;
    }
}
