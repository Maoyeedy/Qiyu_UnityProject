using UnityEngine;

public class FadeOnRespawnScene2 : Fade
{
    public float killFloorHeight = -2f, respawnDelay = 0.5f, zeroToOneDuration = 0.1f;
    public GameObject text;
    private GameObject _player;
    private bool _isDead;

    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        _player = GameObject.FindWithTag("Player");
        FadeInDelayed();
    }

    private void Update()
    {
        if (CheckRespawn())
            if (!_isDead)
                FadeWhite();

        _isDead = _player.transform.position.y < killFloorHeight;
    }

    private bool CheckRespawn()
    {
        if (_player.transform.position.y < killFloorHeight)
            return true;
        return false;
    }

    public void FadeWhite()
    {
        text.SetActive(false);
        LeanTween.value(gameObject, SetAlpha, 0f, 1f, zeroToOneDuration);
        Invoke(nameof(FadeIn), respawnDelay + 0.25f);
    }
}