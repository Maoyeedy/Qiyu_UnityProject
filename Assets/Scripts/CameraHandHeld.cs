using UnityEngine;

public class HandheldCamera : MonoBehaviour
{
    public float shakeAmount = 0.5f;
    public float shakeDuration = 5f, shakeSpeed = 0.05f, randomFactor = 0.5f;
    private float _timer;
    private Vector3 _targetPosition;

    private void Start()
    {
        _timer = shakeDuration;
        Shake();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            Shake();
            _timer = shakeDuration;
        }

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * shakeSpeed);
    }

    private void Shake()
    {
        var shakeOffsetZ = Random.value * shakeAmount * randomFactor - shakeAmount;
        var shakeOffsetY = Random.value * shakeAmount * randomFactor - shakeAmount;

        var position = transform.position;
        _targetPosition = new Vector3(position.x, position.y + shakeOffsetY, position.z + shakeOffsetZ);
    }
}