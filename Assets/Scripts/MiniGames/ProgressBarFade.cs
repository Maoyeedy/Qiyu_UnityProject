using UnityEngine;

public class ProgressFade : Fade
{
    private bool _initialized;

    private void Start()
    {
        CanvasGroup = gameObject.GetComponent<CanvasGroup>();
        fadeDuration = 0.25f;
        SetAlpha(0);
    }
}