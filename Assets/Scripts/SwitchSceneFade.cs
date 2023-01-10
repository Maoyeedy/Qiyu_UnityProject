using UnityEngine;

public class SwitchSceneFade : Fade
{
    public GameObject onScreenUI;

    private void Start()
    {
        onScreenUI.SetActive(false);
        CanvasGroup = GetComponent<CanvasGroup>();
        FadeOut();
    }
}