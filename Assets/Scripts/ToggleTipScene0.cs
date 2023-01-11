using UnityEngine;

public class ToggleTipScene0 : Fade
{
    private bool _returnPressed, _leftClicked, _rightClicked;
    public bool isTip1, isTip2;
    public GameObject tip2;

    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        if (isTip2)
            FadeOutDelayed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            _returnPressed = true;
        if (Input.GetMouseButtonDown(0))
            _leftClicked = true;
        if (Input.GetMouseButtonDown(1))
            _rightClicked = true;

        if (isTip1)
            if (_returnPressed || _leftClicked)
            {
                tip2.SetActive(true);
                FadeTip();
            }

        if (isTip2)
            if (_rightClicked)
            {
                fadeDuration = 1.25f;
                FadeTip();
            }
    }

    private void FadeTip()
    {
        Invoke(nameof(DestroyGameObject), fadeDuration);
        FadeIn();
        _returnPressed = false;
        _leftClicked = false;
        _rightClicked = false;
    }
}
