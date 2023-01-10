using UnityEngine;

public class FadeOnRespawnScene1 : Fade
{
    public float killFloorHeight = -2f;
    public GameObject text;
    private GameObject _player;

    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        _player = GameObject.FindWithTag("Player");
        FadeInDelayed();
    }

    private void Update()
    {
        if (_player.transform.position.y < killFloorHeight)
        {
            text.SetActive(false);
            SetAlpha(1);
            Invoke(nameof(FadeIn), fadeDelay);
        }
    }
}