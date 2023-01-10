using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTimeCounter : MonoBehaviour
{
    public string key = "PlayTime", scene1Name = "Scene 1";
    public float playTime;
    public bool realTimeDisplay = true;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        playTime = PlayerPrefs.GetFloat(key, 0f);
        if (SceneManager.GetActiveScene().name == scene1Name)
            playTime = 0f;
    }

    private void Update()
    {
        playTime += Time.deltaTime;
        if (realTimeDisplay)
            timerText.text = $"{playTime:F1}\"";
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(key, playTime);
    }
}