using UnityEngine;

public class ToggleOnScreenTip : MonoBehaviour
{
    public GameObject tipText;
    private bool _inRange;
    private Penguin _penguin;
    private Fade _text;

    private void Start()
    {
        _penguin = GetComponent<Penguin>();
        _text = tipText.GetComponent<Fade>();
    }

    private void Update()
    {
        if (!_inRange)
            if (_penguin.distanceToGameObject <= 5f)
            {
                _inRange = true;
                _text.TempDisplay();
            }
    }
}