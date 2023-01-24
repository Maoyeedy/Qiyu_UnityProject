using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RespawnPointUpdate : MonoBehaviour
{
    public float updateRange = 6f;
    public GameObject respawnPoint;
    public bool zoomInWhenUpdate;
    public CameraZoom zoom;
    public bool switchCamWhenUpdate;
    public GameObject cam1, cam2;
    public FadeOnRespawnScene2 fade;
    public float switchCamDuration = 2.5f;
    public float apertureDelta = 0.2f;
    public float focusDelta = -0.1f;
    private DepthOfField _depthOfField;

    private float _distanceToGameObject;
    private bool _hasZoomed, _hasSwitched, _dofStatus, _hasUpdated;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (switchCamWhenUpdate)
            _depthOfField = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>();
    }

    private void Update()
    {
        if (_hasUpdated)
            return;

        _distanceToGameObject = Vector3.Distance(transform.position, _player.transform.position);
        if (_distanceToGameObject <= updateRange)
        {
            var transform1 = transform;
            var position = transform1.position;
            respawnPoint.transform.position = new Vector3(position.x, respawnPoint.transform.position.y, position.z);
            respawnPoint.transform.rotation = transform1.rotation;

            if (zoomInWhenUpdate)
                zoom.TempZoomIn();

            if (switchCamWhenUpdate)
                StartCoroutine(SwitchCam());

            _hasUpdated = true;
        }
    }

    private IEnumerator SwitchCam()
    {
        if (!_hasSwitched)
        {
            _hasSwitched = true;
            fade.fadeDuration /= 2;
            fade.zeroToOneDuration *= 2;
            fade.FadeWhite();
            yield return new WaitForSeconds(fade.zeroToOneDuration);
            _depthOfField.aperture.value += apertureDelta;
            _depthOfField.focusDistance.value += focusDelta;
            _dofStatus = _depthOfField.active;
            _depthOfField.active = true;
            cam1.SetActive(false);
            cam2.SetActive(true);
            yield return new WaitForSeconds(switchCamDuration);
            fade.FadeWhite();
            yield return new WaitForSeconds(fade.zeroToOneDuration);
            _depthOfField.aperture.value -= apertureDelta;
            _depthOfField.focusDistance.value -= focusDelta;
            _depthOfField.active = _dofStatus;
            cam1.SetActive(true);
            cam2.SetActive(false);
            fade.fadeDuration *= 2;
            fade.zeroToOneDuration /= 2;
        }
    }
}