using TMPro;
using UnityEngine;

public class PlayTimeDisplay : PlayTimeCounter
{
    public TextMeshProUGUI finalText;
    public float duration = 3.0f;
    private bool _isCounting = true;
    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
        playTime = PlayerPrefs.GetFloat(key, 0f);
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_isCounting)
        {
            var currentTime = Time.time;
            var elapsedTime = currentTime - _startTime;
            var t = elapsedTime / duration;

            var currentValue = Mathf.Lerp(0f, playTime, t);
            finalText.text = $"{currentValue:0.0}\"";

            if (Mathf.Abs(currentValue - playTime) < 0.01f)
                _isCounting = false;
        }
    }
}