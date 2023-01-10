using UnityEngine;

public class ScannerSwitcher : SwitchCamera
{
    public GameObject scanner;

    private void Update()
    {
        DestroyDialog();
        if (Input.GetKeyDown(key))
        {
            scanner.SetActive(true);
            SwitchCam();
        }
    }
}