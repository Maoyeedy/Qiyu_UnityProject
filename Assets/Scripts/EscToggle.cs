using UnityEngine;

public class EscToggleUI : MonoBehaviour
{
    public GameObject escUI, cam;
    public vThirdPersonCamera thirdPersonCam;
    public bool escFreezesTime = true, escHidesOtherUI = true;
    private bool _uiOn;
    private GameObject[] _ui;

    private void Start()
    {
        Cursor.visible = false;

        if (escFreezesTime && thirdPersonCam == null)
        {
            cam = GameObject.Find("vThirdPersonCamera");
            thirdPersonCam = cam.GetComponent<vThirdPersonCamera>();
        }

        if (escHidesOtherUI)
            _ui = GameObject.FindGameObjectsWithTag("UI");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            ToggleUI();
    }

    public void ToggleUI()
    {
        _uiOn = !_uiOn;
        Cursor.visible = _uiOn;
        escUI.SetActive(_uiOn);

        if (escFreezesTime)
        {
            GameObject.Find("vThirdPersonCamera").GetComponent<CameraZoom>().escOn = _uiOn;
            thirdPersonCam.lockCamera = _uiOn;
            Time.timeScale = _uiOn ? 0 : 1;
        }

        if (escHidesOtherUI)
            foreach (var x in _ui)
                x.SetActive(!_uiOn);
    }

    public void HideUI()
    {
        foreach (var x in _ui) x.SetActive(false);
    }
}