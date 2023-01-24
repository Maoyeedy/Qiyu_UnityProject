using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI dialogText, nameText;
    public string[] lines, names;
    public float characterInterval = 0.66f, autoNextLineTime = 2.5f;
    public bool dialogHaveNames;

    [Header("This is for Scene 0")] public bool isIntroDialog;
    public GameObject player;
    public Fade fade;
    [Header("This is for Scene 1")] public bool isPenguin1;
    public GameObject initialUI;
    public GameObject penguin1, penguin2;
    [Header("This is for PenguinSoul")] public bool isPenguinSoul;
    public PenguinSoul penguinSoul;
    public float apertureDelta = 0.2f;
    public float focusDelta = -0.1f;

    private int _index;
    private float _elapsedTime;
    private bool _uiOn, _dofStatus, _dialogFinished;
    private DepthOfField _depthOfField;

    private void Start()
    {
        dialogText.text = string.Empty;
        StartDialog();
        UpdateName();
        if (isPenguinSoul)
        {
            penguinSoul.SwitchCam();
            _depthOfField = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>();
            _dofStatus = _depthOfField.active;
            _depthOfField.active = true;
            _depthOfField.aperture.value += apertureDelta;
            _depthOfField.focusDistance.value += focusDelta;
        }

        if (isPenguin1) initialUI.SetActive(false);
    }

    private void Update()
    {
        if (_dialogFinished)
            return;

        _elapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Backspace)) _uiOn = !_uiOn;

        if ((Input.GetMouseButtonDown(0) && !_uiOn) || Input.GetKeyDown(KeyCode.Return)) SwitchLine();

        if (_elapsedTime > autoNextLineTime && dialogText.text == lines[_index])
        {
            _elapsedTime = 0;
            NextLine();
        }

        if (Input.GetKeyDown(KeyCode.X))
            NextLine();
    }

    private void StartDialog()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (var x in lines[_index].ToCharArray())
        {
            dialogText.text += x;
            yield return new WaitForSeconds(characterInterval);
        }
    }

    private void SwitchLine()
    {
        if (dialogText.text == lines[_index])
        {
            _elapsedTime = 0;
            NextLine();
        }
        else
        {
            _elapsedTime = 0;
            StopAllCoroutines();
            dialogText.text = lines[_index];
        }
    }

    private void NextLine()
    {
        dialogText.text = string.Empty;
        if (_index < lines.Length - 1)
        {
            _index++;
            StartCoroutine(TypeLine());
            UpdateName();
        }
        else
        {
            DialogEnds();
        }
    }

    private void UpdateName()
    {
        if (dialogHaveNames)
            nameText.text = names[_index];
    }

    private void DialogEnds()
    {
        if (isPenguin1)
        {
            penguin1.SetActive(false);
            penguin2.SetActive(true);
        }

        if (dialogHaveNames)
            nameText.text = string.Empty;

        if (isIntroDialog)
        {
            dialogText.text = lines[^1];
            nameText.text = names[^1];
            StartCoroutine(IntroDialog());
            return;
        }

        if (isPenguinSoul)
        {
            _depthOfField.aperture.value -= apertureDelta;
            _depthOfField.focusDistance.value -= focusDelta;
            _depthOfField.active = _dofStatus;
            penguinSoul.SwitchCam();
            penguinSoul.followPlayer = true;
            penguinSoul.pingPongFactor = 0.075f;
            transform.parent.gameObject.SetActive(false);
            return;
        }

        if (isPenguin1)
        {
            penguin1.SetActive(false);
            penguin2.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    private IEnumerator IntroDialog()
    {
        _dialogFinished = true;
        fade.fadeDuration = 0.5f;
        fade.TempDisplay();
        yield return new WaitForSeconds(fade.fadeDuration + fade.tempDisplayTime);
        player.SetActive(true);
        GameObject.Find("IntroDialogs").SetActive(false);
    }
}