using UnityEngine;

public class OnScreenUIToggle : MonoBehaviour
{
    public GameObject onScreenUI, progressBar, dialog, music;
    public bool uiIsOn = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Tab))
        {
            uiIsOn = !uiIsOn;
            onScreenUI.SetActive(uiIsOn);
            progressBar.SetActive(uiIsOn);
            music.SetActive(uiIsOn);
            dialog.SetActive(uiIsOn);
        }
    }
}
