using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    public int scorePerGame = 25;
    public GameObject finishDialog;
    public RectMask2D mask;

    private float _transitionTime;
    private bool _dnaFinished;
    private int _internalScoreCount;
    private TextMeshProUGUI _goalText;

    private void Start()
    {
        _goalText = gameObject.GetComponent<TextMeshProUGUI>();
        _transitionTime = 1f;
    }

    public void UpdateScore()
    {
        _internalScoreCount += scorePerGame;
        var vector = mask.padding;
        vector.x = Mathf.Max(vector.x, 0);

        var startValue = vector.x;
        var endValue = startValue + scorePerGame * 8;

        LeanTween.value(startValue, endValue, _transitionTime)
            .setOnUpdate(val =>
            {
                vector.x = val;
                mask.padding = vector;
            });

        _goalText.text = $"收集进度：{Mathf.Min(_internalScoreCount, 100)}%";

        if (_internalScoreCount == 100) finishDialog.SetActive(true);
    }
}