using TMPro;
using UnityEngine;
using System;

public class Scanner : MonoBehaviour
{
    public float rotateSpeed = 30f, keyPressFactor = 15f;
    public TextMeshProUGUI progressText, tipText;
    public Camera camMain, camScanner;
    public GameObject scanner;
    public Progress progress;
    public ProgressFade progressFade;

    private float _currentRotation, _progress;
    private bool _gameFinished;

    private void Update()
    {
        var rotation = 0f;

        if (Input.GetKey(KeyCode.E) && _progress < 100)
            if (Input.GetKeyDown(KeyCode.Q))
                rotation += rotateSpeed;

        _progress += rotation * Time.deltaTime * keyPressFactor / 3.6f;
        transform.Rotate(Vector3.up, rotation * Time.deltaTime * keyPressFactor);

        _progress = Math.Min(_progress, 100);
        progressText.text = $"扫描进度：{_progress:F1} %";

        if (Math.Abs(_progress - 100) < 0.01f)
        {
            _gameFinished = true;
            tipText.text = "扫描完成\n请按 Tab 退出扫描";
        }

        if (Input.GetKeyDown(KeyCode.Tab) && _gameFinished)
        {
            _progress = 0;
            tipText.text = "先按下E\n再按下Q\n以进行三维扫描";

            camScanner.enabled = false;
            camMain.enabled = true;
            Cursor.visible = false;
            progressFade.TempDisplayProgressBar();
            progress.UpdateScore();
            scanner.SetActive(false);
        }
    }
}
