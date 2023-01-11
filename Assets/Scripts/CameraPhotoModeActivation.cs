using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivatePhotoMode : MonoBehaviour
{
    [Header("README")] public string 切换方法 = "BackSpace - 切换PhotoMode\nShift+1 - 切至Scene0\nShift+2 - 切至Scene1\nShift+3 - 切至Scene2\nShift+4 - 切至Outro";
    public string 操作方式 = "截图 - Tab\n移动相机位置 - WASDQE\n移动相机视角 - 按住右键并移动鼠标\n改变相机视野与移速 - 滑动鼠标滚轮";
    [Header("Settings")] public GameObject photoModeCam;
    public bool scene0DisableIntroTimeline;
    private bool _photoModeOn;
    private CameraPhotoMode _cam;
    private EscToggleUI _esc;

    private void Start()
    {
        _esc = GameObject.Find("EscToggleUI").GetComponent<EscToggleUI>();
        _cam = photoModeCam.GetComponent<CameraPhotoMode>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Load("Scene 0");
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Load("Scene 1");
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Load("Scene 2");
            if (Input.GetKeyDown(KeyCode.Alpha4))
                Load("Outro");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _esc.escFreezesTime = _photoModeOn;
            _esc.escHidesOtherUI = _photoModeOn;
            _esc.ToggleUI();
            _esc.HideUI();

            if (!_photoModeOn)
            {
                photoModeCam.SetActive(!_photoModeOn);
                _cam.ToggleThirdPersonPlayer();
            }

            if (_photoModeOn)
            {
                _cam.ToggleThirdPersonPlayer();
                photoModeCam.SetActive(!_photoModeOn);
            }

            _photoModeOn = !_photoModeOn;

            if (scene0DisableIntroTimeline)
                Destroy(GameObject.Find("IntroTimeline"));
        }
    }

    private void Load(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
