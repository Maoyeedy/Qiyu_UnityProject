using UnityEngine;
using UnityEngine.UI;

public class ProgressBlackToRed : MonoBehaviour
{
    private Graphic _graphic;
    private bool _initialized;

    private void Start()
    {
        _graphic = GetComponent<Graphic>();
        _graphic.color = Color.black;
    }


    private void Update()
    {
        if (!_initialized)
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0))
            {
                _initialized = true;
                Invoke(nameof(BlackToRed), 1f);
            }
    }

    private void BlackToRed()
    {
        _graphic.color = Color.white;
    }
}
