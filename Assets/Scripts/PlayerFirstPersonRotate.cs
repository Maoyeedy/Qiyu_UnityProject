using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float sensitivity = 2.0f;

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * sensitivity;
        transform.localRotation = Quaternion.AngleAxis(mouseX, Vector3.up);
    }
}