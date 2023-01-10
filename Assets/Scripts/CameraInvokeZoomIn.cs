using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public CameraZoom zoom;
    public float range;
    private GameObject _player;
    private float _distanceToGameObject;
    private bool _hasEnteredRange;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (_hasEnteredRange)
            return;

        _distanceToGameObject = Vector3.Distance(transform.position, _player.transform.position);
        if (_distanceToGameObject <= range)
        {
            _hasEnteredRange = true;
            zoom.TempZoomIn();
        }
    }
}