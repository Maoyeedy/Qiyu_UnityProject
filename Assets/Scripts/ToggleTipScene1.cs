using UnityEngine;

public class ToggleTipScene1 : Fade
{
    private bool _spacePressed, _shiftPressed;

    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _spacePressed = true;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _shiftPressed = true;

        if (_spacePressed && _shiftPressed)
            FadeTip();
    }

    private void FadeTip()
    {
        Invoke(nameof(DestroyGameObject), fadeDuration);
        FadeIn();
        _spacePressed = false;
        _shiftPressed = false;
    }
}