using System;
using System.IO;
using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class CameraPhotoMode : MonoBehaviour
{
    public string 使用方法 = "按Tab保存截图至D:\\Screenshots，路径可自定义。\nUI将在按下Tab时被禁用一帧。";

    [Header("WASDQE Move")] public float moveSpeed = 8f;
    public float heightSpeed = 6f;
    public float mouseSensitivity = 4f;
    [Header("Other Setting")] public string screenshotDirectory = "D:\\Screenshots";
    public bool disableThirdPersonPlayer;
    private int _screenshotIndex;
    private string _scene;
    private Camera _mainCamera;
    private Transform _transform;
    private FloatParameter _focalLength;
    private float _defaultFocalLength;
    private GameObject[] _ui;
    private bool _thirdPersonDisabled;


    private void Start()
    {
        _focalLength = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>().focalLength;
        _defaultFocalLength = _focalLength.value;
        _mainCamera = GetComponent<Camera>();
        _transform = transform;

        _scene = SceneManager.GetActiveScene().name;
        screenshotDirectory = Path.Combine(screenshotDirectory, _scene);
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
            Scroll();

        if (Input.GetMouseButton(1))
            Rotate();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _ui = GameObject.FindGameObjectsWithTag("UI");
            DisableUI();
            Screenshot();
        }

        Move();
    }

    private void Scroll()
    {
        var scrollTemp = Input.mouseScrollDelta.y;
        var speedFactor = 0.125f;
        var focusLengthFactor = 0.5f;

        _mainCamera.fieldOfView = Mathf.Clamp(_mainCamera.fieldOfView - scrollTemp, 24, 120);
        moveSpeed = Mathf.Clamp(moveSpeed - scrollTemp * speedFactor, 0.75f, 3.9f);
        heightSpeed = Mathf.Clamp(heightSpeed - scrollTemp * speedFactor, 0.5f, 2.6f);

        var value = _focalLength.value;
        value += scrollTemp * focusLengthFactor;
        value = MathF.Max(_defaultFocalLength, value);
        _focalLength.value = value;
    }

    private void Rotate()
    {
        var mouseInputX = Input.GetAxis("Mouse X");
        var mouseInputY = Input.GetAxis("Mouse Y");

        Transform transform2;
        (transform2 = transform).Rotate(-mouseInputY * mouseSensitivity, mouseInputX * mouseSensitivity, 0);

        var rotation = transform2.rotation;
        rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }

    private void Screenshot()
    {
        _screenshotIndex++;

        var filename = $"{_scene} {_screenshotIndex:000}.png";
        var screenshotPath = Path.Combine(screenshotDirectory, filename);

        if (!Directory.Exists(screenshotDirectory))
            Directory.CreateDirectory(screenshotDirectory);

        ScreenCapture.CaptureScreenshot(screenshotPath);
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
            _transform.position += _transform.forward * (moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            _transform.position -= _transform.forward * (moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            _transform.position -= _transform.right * (moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            _transform.position += _transform.right * (moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q))
            _transform.position -= _transform.up * (heightSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            _transform.position += _transform.up * (heightSpeed * Time.deltaTime);
    }

    public void ToggleThirdPersonPlayer()
    {
        if (disableThirdPersonPlayer)
        {
            GameObject.FindWithTag("Player").GetComponent<vThirdPersonInput>().enabled = _thirdPersonDisabled;
            GameObject.Find("vThirdPersonCamera").GetComponent<Camera>().enabled = _thirdPersonDisabled;
            _thirdPersonDisabled = !_thirdPersonDisabled;
        }
    }

    private void DisableUI()
    {
        foreach (var x in _ui) x.SetActive(false);
        StartCoroutine(DelayOneFrame());
    }

    private System.Collections.IEnumerator DelayOneFrame()
    {
        yield return null;
        foreach (var x in _ui) x.SetActive(true);
    }
}
