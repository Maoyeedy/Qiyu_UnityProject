using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera camMain, camTarget;
    public KeyCode key = KeyCode.Z;
    [Header("Destroy Dialog")] public bool destroyOnZ;
    public GameObject dialog, finishDialog;
    public GameObject penguin;

    private void Update()
    {
        if (Input.GetKeyDown(key)) SwitchCam();
    }

    protected void SwitchCam()
    {
        camMain.enabled = false;
        camTarget.enabled = true;
    }

    protected void DestroyDialog()
    {
        if (destroyOnZ)
            if (Input.GetKeyDown(KeyCode.Z))
            {
                finishDialog.SetActive(true);
                Destroy(dialog);
                Destroy(penguin);
                Destroy(gameObject);
            }
    }
}
