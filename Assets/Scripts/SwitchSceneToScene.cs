using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SceneToScene : SwitchScene
{
    public PostProcessVolume postProcessVolume;
    public float transitionTime = 3f, freezePlayerDelay = 0.15f, distanceDelta = 0.4f;
    public SwitchSceneFade switchSceneFade;
    public GameObject fadeOut;
    public AudioSource portalSound;
    public bool stopSceneMusic = true;
    public AudioSource sceneMusic;
    public float musicFadeOutTime = 3f;
    [HideInInspector] public GameObject player;

    [Header("Destroy All Dialogs")] public bool disableUIOnEnter;
    public GameObject ui;

    private vThirdPersonCamera _cam;
    private vThirdPersonInput _input;
    private ChromaticAberration _chromaticAberration;
    private LensDistortion _lensDistortion;
    private bool _hasEntered;

    private void Start()
    {
        postProcessVolume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
        GetChromaticAberration();
        GetLensDistortion();

        _cam = GameObject.Find("vThirdPersonCamera").GetComponent<vThirdPersonCamera>();
        player = GameObject.FindWithTag("Player");
        _input = player.GetComponent<vThirdPersonInput>();

        switchSceneFade.fadeDuration = transitionTime - 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            portalSound.Play();

            _hasEntered = true;

            _cam.lockCamera = true;
            ZoomIn();

            Invoke(nameof(LoadScene), transitionTime);
            Invoke(nameof(FreezePlayer), freezePlayerDelay);

            fadeOut.SetActive(true);
            LeanTween.value(gameObject, UpdateAberrationIntensity, 0f, 1.5f, transitionTime);
            LeanTween.value(gameObject, UpdateDistortionIntensity, 0f, 50f, transitionTime);

            if (disableUIOnEnter)
                ui.SetActive(false);
        }
    }

    private void Update()
    {
        if (stopSceneMusic && _hasEntered)
            SceneMusicFadeOut();
    }

    private void FreezePlayer()
    {
        var playerRigidbody = player.GetComponent<Rigidbody>();
        playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        var playerAnimator = player.GetComponent<Animator>();
        playerAnimator.enabled = false;

        // _input.horizontalInput = null;
        // _input.verticallInput = null;
    }

    private void GetChromaticAberration()
    {
        _chromaticAberration = postProcessVolume.profile.GetSetting<ChromaticAberration>();
    }

    private void GetLensDistortion()
    {
        _lensDistortion = postProcessVolume.profile.GetSetting<LensDistortion>();
    }

    private void UpdateAberrationIntensity(float intensity)
    {
        _chromaticAberration.intensity.value = intensity;
    }

    private void UpdateDistortionIntensity(float intensity)
    {
        _lensDistortion.intensity.value = intensity;
    }

    private void ZoomIn()
    {
        var tempDistance = _cam.defaultDistance;

        LeanTween.value(tempDistance, tempDistance - distanceDelta, transitionTime)
            .setOnUpdate(val => { _cam.defaultDistance = val; });
    }


    private void SceneMusicFadeOut()
    {
        sceneMusic.volume = Mathf.Lerp(sceneMusic.volume, 0f, Time.deltaTime / musicFadeOutTime);
    }
}
