using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraZoom : MonoBehaviour
{
    [Header("FOV")] public float fovRange = 8f;
    public float fovDelta = 4f;

    [Header("Camera Distance")] public float distanceRange = 0.25f;
    public float distanceDelta = 0.2f;

    [Header("Factors")] public float zoomDuration = 0.3f;
    public float zoomInFactor = 1.5f;
    public float tempZoomInDuration = 1.5f;
    public float tempZoomInFactor = 1.25f;
    public float rightOffsetFactor = 0.3f;
    public bool canScroll, canRun = true, escOn;
    public float scrollFactor = 0.025f;

    private vThirdPersonCamera cam;
    private DepthOfField depthOfField;
    private float FOV, FOVmin, FOVmax, FOVdefault, tempFOV, tempDistance, tempFocusDistance, defaultDistance, defaultRightOffset;
    private bool isZoomingIn, isZoomingToDefault;

    private void Start()
    {
        depthOfField = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>();
        tempFocusDistance = depthOfField.focusDistance.value;

        cam = GetComponent<vThirdPersonCamera>();
        defaultDistance = cam.defaultDistance;
        defaultRightOffset = cam.rightOffset;
        tempDistance = defaultDistance;

        FOV = GetComponent<Camera>().fieldOfView;
        FOVdefault = FOV;
        tempFOV = FOV;
        SetMinMax();
    }

    private void Update()
    {
        if (escOn)
            return;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            if (Input.GetKeyDown(KeyCode.LeftShift) && canRun)
                ZoomOut();

        if (Input.GetMouseButtonDown(1) && !isZoomingIn)
        {
            isZoomingIn = true;
            ZoomIn();
            Invoke(nameof(ZoomInFinished), zoomDuration * zoomInFactor);
        }

        if ((Input.GetKeyUp(KeyCode.LeftShift) && canRun) || Input.GetMouseButtonUp(1))
            ZoomToDefault();

        if (canScroll)
            if (Input.mouseScrollDelta.y != 0)
                ZoomOnMouseScroll();
    }

    public void ZoomIn()
    {
        tempFOV = FOV;
        FOVmin -= fovDelta * 2f;

        LeanTween.value(FOV, FOV - fovDelta * 2f, zoomDuration * zoomInFactor).setOnUpdate(val =>
        {
            FOV = val;
            GetComponent<Camera>().fieldOfView = FOV;
        });

        LeanTween.value(tempDistance, tempDistance - distanceDelta * 1.75f, zoomDuration * zoomInFactor).setOnUpdate(val => { cam.defaultDistance = val; });
        LeanTween.value(tempFocusDistance, tempFocusDistance - distanceDelta * 1.75f, zoomDuration * zoomInFactor).setOnUpdate(val => { depthOfField.focusDistance.value = val; });
        LeanTween.value(defaultRightOffset, defaultRightOffset + distanceDelta * rightOffsetFactor, zoomDuration * zoomInFactor).setOnUpdate(val => { cam.rightOffset = val; });
    }

    public void ZoomOut()
    {
        tempFOV = FOV;
        FOVmax += fovDelta;

        LeanTween.value(FOV, FOV + fovDelta, zoomDuration).setOnUpdate(val =>
        {
            FOV = val;
            GetComponent<Camera>().fieldOfView = FOV;
        });

        LeanTween.value(tempDistance, tempDistance - distanceDelta, zoomDuration).setOnUpdate(val => { cam.defaultDistance = val; });
        LeanTween.value(tempFocusDistance, tempFocusDistance - distanceDelta, zoomDuration).setOnUpdate(val => { depthOfField.focusDistance.value = val; });
    }

    public void ZoomToDefault()
    {
        LeanTween.value(FOV, tempFOV, zoomDuration).setOnUpdate(val =>
        {
            FOV = val;
            GetComponent<Camera>().fieldOfView = FOV;
        });

        LeanTween.value(cam.defaultDistance, tempDistance, zoomDuration).setOnUpdate(val => { cam.defaultDistance = val; });
        LeanTween.value(depthOfField.focusDistance.value, tempFocusDistance, zoomDuration).setOnUpdate(val => { depthOfField.focusDistance.value = val; });
        LeanTween.value(cam.rightOffset, defaultRightOffset, zoomDuration).setOnUpdate(val => { cam.rightOffset = val; });

        SetMinMax();
    }

    public void TempZoomIn()
    {
        canRun = false;
        zoomDuration *= 0.75f;
        fovDelta *= tempZoomInFactor;
        distanceDelta *= tempZoomInFactor;
        ZoomIn();
        Invoke(nameof(ZoomToDefault), tempZoomInDuration);
        Invoke(nameof(ResetCanRun), tempZoomInDuration);
        zoomDuration /= 0.75f;
        fovDelta *= tempZoomInFactor;
        distanceDelta /= tempZoomInFactor;
    }

    private void ZoomOnMouseScroll()
    {
        var scrollDelta = Input.mouseScrollDelta.y;

        tempDistance -= scrollDelta * scrollFactor;
        tempDistance = Mathf.Clamp(tempDistance, defaultDistance - distanceRange, defaultDistance + distanceRange);
        cam.defaultDistance = tempDistance;

        FOV -= scrollDelta * scrollFactor * 20;
        FOV = Mathf.Clamp(FOV, FOVmin, FOVmax);
        GetComponent<Camera>().fieldOfView = FOV;
    }

    private void ZoomInFinished()
    {
        isZoomingIn = false;
    }

    private void SetMinMax()
    {
        FOVmax = FOVdefault + fovRange;
        FOVmin = FOVdefault - fovRange * 0.75f;
    }

    private void ResetCanRun()
    {
        canRun = true;
    }
}