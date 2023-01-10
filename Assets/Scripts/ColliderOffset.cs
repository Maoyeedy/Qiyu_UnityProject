using UnityEngine;

public class ColliderOffset : MonoBehaviour
{
    public float offset = -0.1f;

    private void Start()
    {
        var temp = gameObject.GetComponent<MeshCollider>();
        temp.transform.Translate(0, offset, 0);
    }
}