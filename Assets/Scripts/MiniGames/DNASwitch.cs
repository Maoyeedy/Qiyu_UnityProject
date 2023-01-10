using UnityEngine;

public class DnaSwitcher : SwitchCamera
{
    public GameObject dna;

    private void Update()
    {
        DestroyDialog();
        if (Input.GetKeyDown(key))
        {
            dna.SetActive(true);
            SwitchCam();
        }
    }
}