using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private const float OriginOffset = .001f;
    public AudioSource landing, jumping;
    public float distanceThreshold = .15f;
    public bool isGrounded = true, haveOceanInScene;
    public LayerMask oceanLayer;
    private bool _isGroundedNow;
    private Vector3 raycastOrigin => transform.position + Vector3.up * OriginOffset;
    private float raycastDistance => distanceThreshold + OriginOffset;

    private void Start()
    {
        if (haveOceanInScene)
            oceanLayer = 1 << LayerMask.NameToLayer("Ocean");
    }

    private void LateUpdate()
    {
        _isGroundedNow = haveOceanInScene ? Physics.Raycast(raycastOrigin, Vector3.down, distanceThreshold * 2, ~oceanLayer) : Physics.Raycast(raycastOrigin, Vector3.down, distanceThreshold * 2);

        if (_isGroundedNow && !isGrounded)
            landing.Play();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            jumping.Play();

        isGrounded = _isGroundedNow;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawLine(raycastOrigin, raycastOrigin + Vector3.down * raycastDistance,
            isGrounded ? Color.white : Color.red);
    }
}
