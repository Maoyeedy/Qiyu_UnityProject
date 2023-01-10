using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class EndScreen : Fade
{
    public GameObject fadeWhite;
    public GameObject thirdPersonPlayer;
    public GameObject firstPersonPlayer;
    public GameObject afterEndScreen;
    private DepthOfField _depthOfField;
    public bool playAudio;
    public AudioSource audioSus;

    private void Start()
    {
        _depthOfField = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>();
        _depthOfField.active = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (playAudio)
                audioSus.Play();

            fadeWhite.SetActive(true);
            Invoke(nameof(LoadScene1), fadeWhite.GetComponent<Fade>().fadeDuration);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playAudio)
                audioSus.Play();

            _depthOfField.active = false;
            _depthOfField.aperture.value = 1.2f;
            thirdPersonPlayer.SetActive(false);
            firstPersonPlayer.SetActive(true);
            afterEndScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void LoadScene1()
    {
        SceneManager.LoadScene("Scene 1");
    }
}