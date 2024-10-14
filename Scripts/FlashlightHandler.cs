using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class FlashlightHandler : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Transform _flashLightForwardTransform;
    [SerializeField]
    private Volume _volume;
    private Vignette _vignette;
    private bool _flashLightEnabled = false;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _screenPoint;
    private Vector2 _normalisedScreenPoint;
    private Coroutine _flashlightCoroutine = null;
    private void Start()
    {
        _volume.profile.TryGet<Vignette>(out _vignette);
    }
    public void EnableFlashlight()
    {

        print("enabling flashlight");
        _volume.enabled = true;
        _volume.priority = 99;
        _vignette.intensity.value = 1;
        _flashLightEnabled = true;
        if (_flashlightCoroutine == null)
        {
            _flashlightCoroutine =  StartCoroutine(IFlashlight());
            print("enabled coroutine");
        }
    }
    public void DisableFlashlight()
    {
        print("disabling flashlight");
        _flashLightEnabled = false;
        _vignette.intensity.value = 0;
        if (_flashlightCoroutine != null)
        {

            StopCoroutine(_flashlightCoroutine);
            _flashlightCoroutine = null;
            print("coroutine stopped");
        }
        _volume.priority = 0;
        _volume.enabled = false;
    }
    private IEnumerator IFlashlight()
    { 
        while (_flashLightEnabled)
        {
            print("flashlightRunning");
            _ray = new Ray(_flashLightForwardTransform.position, _flashLightForwardTransform.forward);
            if (Physics.Raycast(_ray, out _hit, 100))
            {
                _screenPoint = _mainCamera.WorldToScreenPoint(_hit.point);
                _normalisedScreenPoint.x = _screenPoint.x / Screen.width;
                _normalisedScreenPoint.y = _screenPoint.y / Screen.height;
                _vignette.center.value = _normalisedScreenPoint;
            }
            yield return null;
        }
        yield return null;
    }
}

