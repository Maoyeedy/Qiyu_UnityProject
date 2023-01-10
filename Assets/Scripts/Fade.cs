using UnityEngine;

public class Fade : MonoBehaviour
{
    public bool fadeInOnStart = true, fadeOutOnStart, tempDisplayOnStart, haveDelay;
    public float fadeDuration = 1.5f, fadeDelay = 0.25f, tempDisplayTime = 0.5f;
    protected CanvasGroup CanvasGroup;

    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();

        if (fadeInOnStart)
        {
            if (haveDelay)
                FadeInDelayed();
            else
                FadeIn();
        }

        if (fadeOutOnStart)
        {
            if (haveDelay)
                FadeOutDelayed();
            else
                FadeOut();
        }

        if (tempDisplayOnStart)
            TempDisplay();
    }

    public void FadeIn()
    {
        LeanTween.value(gameObject, SetAlpha, 1f, 0f, fadeDuration);
    }

    public void FadeInDelayed()
    {
        SetAlpha(1);
        Invoke(nameof(FadeIn), fadeDelay);
    }

    public void FadeOut()
    {
        LeanTween.value(gameObject, SetAlpha, 0f, 1f, fadeDuration);
    }

    public void FadeOutDelayed()
    {
        SetAlpha(0);
        Invoke(nameof(FadeOut), fadeDelay);
    }

    public void TempDisplay()
    {
        FadeOut();
        Invoke(nameof(FadeIn), fadeDuration + tempDisplayTime);
    }

    public void TempDisplayProgressBar()
    {
        SetAlpha(1);
        Invoke(nameof(FadeIn), fadeDuration + tempDisplayTime);
    }

    public void SetAlpha(float alpha)
    {
        CanvasGroup.alpha = alpha;
    }

    public void DestroyGameObject()
    {
        gameObject.SetActive(false);
    }
}