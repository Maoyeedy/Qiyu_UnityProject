using UnityEngine;

public class FadeIn3D : MonoBehaviour
{
    public float waitForSeconds = 4f;
    public float fadeDuration = 2f;
    public bool haveCollider;
    public bool fadeOutOnStart;
    public bool fadeInOnStart;
    private Material _material;
    private Color _color;
    private Collider _collider;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _color = _material.color;

        if (haveCollider)
        {
            _collider = GetComponent<MeshCollider>();
            _collider.enabled = false;
        }

        if (fadeInOnStart)
        {
            UpdateAlpha(0);
            StartCoroutine(FadeIn());
        }
        else if (fadeOutOnStart)
        {
            UpdateAlpha(1);
            StartCoroutine(FadeOut());
        }
    }

    public System.Collections.IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(waitForSeconds);
        LeanTween.value(gameObject, UpdateAlpha, 0f, 1f, fadeDuration);
        if (haveCollider)
            _collider.enabled = true;
    }

    public System.Collections.IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(waitForSeconds);
        LeanTween.value(gameObject, UpdateAlpha, 1f, 0f, fadeDuration);
        if (haveCollider)
            _collider.enabled = false;
    }

    private void UpdateAlpha(float alpha)
    {
        _color.a = alpha;
        _material.color = _color;
    }
}