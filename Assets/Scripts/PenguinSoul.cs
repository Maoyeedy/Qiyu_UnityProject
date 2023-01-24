using UnityEngine;

public class PenguinSoul : DialogDisplay
{
    [Header("PenguinSoul Control")] public GameObject followTarget;
    public bool followPlayer = true;
    public float followDistance = 2f;
    public float followSpeed = 0.75f;
    public float yOffset = 1f, xOffset = -0.5f, zOffset;
    public float pingPongFactor = 0.15f, pingPongLoop = 2.5f;
    public bool isOutro;
    public GameObject cam1, cam2;
    private Quaternion _defaultRotation;
    private Vector3 _defaultTransform, _desiredPosition;
    private float _defaultYOffset, _defaultPingPongFactor;
    private bool _dialogIsPlaying;

    private void Start()
    {
        GetDefaultTransform();
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (!displayDialog)
        {
            Follow();
            Transform();
            Look();
            return;
        }

        if (InRange)
            _dialogIsPlaying = true;

        if (!_dialogIsPlaying)
            CalculateDistance();

        if (followPlayer)
            Follow();
        else
            PingPong();

        Transform();

        if (isOutro)
        {
            Look();
            return;
        }

        if (dialog.activeInHierarchy)
            transform.rotation = _defaultRotation;
        else
            Look();
    }

    private void Follow()
    {
        yOffset = Mathf.PingPong(Time.time, pingPongLoop) * pingPongFactor + _defaultYOffset;
        _desiredPosition = followTarget.transform.position - followTarget.transform.forward * followDistance + new Vector3(xOffset, yOffset, zOffset);
    }

    private void PingPong()
    {
        yOffset = Mathf.PingPong(Time.time, pingPongLoop) * pingPongFactor;
        _desiredPosition = _defaultTransform + new Vector3(0, yOffset, 0);
    }

    private void Transform()
    {
        transform.position = Vector3.Lerp(transform.position, _desiredPosition, followSpeed * Time.deltaTime);
    }

    private void Look()
    {
        transform.LookAt(followTarget.transform);
    }

    private void GetDefaultTransform()
    {
        _defaultYOffset = yOffset;
        var transform1 = transform;
        _defaultTransform = transform1.position;
        _defaultRotation = transform1.rotation;
    }

    public void SwitchCam()
    {
        Player.SetActive(!Player.activeInHierarchy);
        cam1.SetActive(!cam1.activeInHierarchy);
        cam2.SetActive(!cam2.activeInHierarchy);
    }
}