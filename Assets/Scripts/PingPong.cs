using UnityEngine;

public class PingPong : MonoBehaviour
{
    public float pingPongFactor = 0.15f, pingPongLoop = 1.25f;
    public float followSpeed = 0.75f;
    private Vector3 _desiredPosition;

    private void Update()
    {
        var yOffset = Mathf.PingPong(Time.time, pingPongLoop) * pingPongFactor * 2 - pingPongFactor;

        var position = transform.position;
        _desiredPosition = position + new Vector3(0, yOffset, 0);
        position = Vector3.Lerp(position, _desiredPosition, followSpeed * Time.deltaTime);
        transform.position = position;
    }
}