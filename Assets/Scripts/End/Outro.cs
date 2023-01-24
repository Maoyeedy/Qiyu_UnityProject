using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class Outro : MonoBehaviour
{
    public GameObject photoMode, esc, fps, fpsCam;
    public string tip1 = "按 Esc 切换至拍照模式", tip2 = "截图将保存至 D:\\Screenshots";
    public TextMeshProUGUI textComponent;
    private bool _inPhotoMode;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            TogglePhotoMode();
    }

    private void TogglePhotoMode()
    {
        _inPhotoMode = !_inPhotoMode;
        textComponent.text = _inPhotoMode ? tip2 : tip1;

        photoMode.SetActive(!photoMode.activeInHierarchy);

        photoMode.transform.position = fpsCam.transform.position;
        photoMode.transform.rotation = fpsCam.transform.rotation;

        esc.SetActive(!esc.activeInHierarchy);
        fps.SetActive(!fps.activeInHierarchy);

        Cursor.visible = !Cursor.visible;
    }
}