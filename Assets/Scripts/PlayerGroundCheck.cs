using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public AudioSource landing, jumping;
    public float distanceThreshold = .15f;
    public bool isGrounded = true, haveOceanInScene;
    public LayerMask oceanLayer;

    private const float OriginOffset = .001f;
    private Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;
    private float RaycastDistance => distanceThreshold + OriginOffset;
    private bool _isGroundedNow;

    private void Start()
    {
        if (haveOceanInScene)
            oceanLayer = 1 << LayerMask.NameToLayer("Ocean");
    }

    private void LateUpdate()
    {
        _isGroundedNow = haveOceanInScene ? Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold * 2, ~oceanLayer) : Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold * 2);

        if (_isGroundedNow && !isGrounded)
            landing.Play();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            jumping.Play();

        isGrounded = _isGroundedNow;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.down * RaycastDistance,
            isGrounded ? Color.white : Color.red);
    }
}
