using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscSettings : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public float sliderFactor = 1f;

    private ScreenSpaceReflections _ssr;
    private DepthOfField _depthOfField;
    private string _sceneName;

    private void Start()
    {
        postProcessVolume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();


        if (CheckPhotoModeCamera())
        {
            GameObject.Find("Slider").GetComponent<Slider>().onValueChanged.AddListener(FocusDistance);
            GameObject.Find("DofToggle").GetComponent<Toggle>().isOn = true;
        }
        else
        {
            GameObject.Find("Slider").SetActive(false);
        }
    }

    public void ToggleSsr(bool isOn)
    {
        _ssr = postProcessVolume.profile.GetSetting<ScreenSpaceReflections>();
        _ssr.active = isOn;
    }

    public void ToggleDof(bool isOn)
    {
        _depthOfField = postProcessVolume.profile.GetSetting<DepthOfField>();
        _depthOfField.active = isOn;
    }

    public void ToggleFog(bool isOn)
    {
        RenderSettings.fog = isOn;
    }

    public void FocusDistance(float newValue)
    {
        _depthOfField = postProcessVolume.profile.GetSetting<DepthOfField>();
        _depthOfField.focusDistance.value = 2 + newValue * sliderFactor;
    }

    private bool CheckPhotoModeCamera()
    {
        if (GameObject.Find("PhotoModeCamera") == null) return false;
        return true;
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        _sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(_sceneName);
    }
}