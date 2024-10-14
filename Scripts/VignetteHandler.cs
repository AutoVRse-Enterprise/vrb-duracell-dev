using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
// here it is assumed that the post processing volume is global and has a vignette effect
public class VignetteHandler : MonoBehaviour
{
    [SerializeField]
    private Volume _volume;
    [SerializeField]
    private float _fadeSpeed, _bloomFadeSpeed;
    [HideInInspector]
    public bool isDarkened = false, isBrightened = true;
    [HideInInspector]
    public Vignette vignette;
    [HideInInspector]
    public Bloom bloom;
    public float bloomIntensity;
    private void Start()
    {
        _volume.profile.TryGet<Vignette>(out vignette);
        _volume.profile.TryGet<Bloom>(out bloom);
    }
    public void ToggleVignette(bool val)
    {
        vignette.active = val;
    }
    public void ToggleBloom(bool val)
    {
        bloom.active = val;
    }
    public void SetVolumePriority(int val)
    {
        _volume.priority = val;
    }
    public void ToggleVolume(bool val)
    {
        _volume.enabled = val;
    }
    public void DarkenArea(bool toggleImmediate = false)
    {
        if (_volume.enabled == false)
            ToggleVolume(true);
        if (vignette.active == false)
            ToggleVignette(true);
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);
        if (toggleImmediate)
        {
            vignette.intensity.value = 0f;
            return;
        }
        StartCoroutine(IDarkenArea());
    }
    public void BrightenArea(bool toggleImmediate = false)
    {
        if (toggleImmediate)
        {
            vignette.intensity.value = 1f;
            if (bloom)
                bloom.intensity.value = 0;
            _volume.enabled = false;
            return;
        }
        StartCoroutine(IBrightenArea());
    }
    private IEnumerator IDarkenArea()
    {
        float priority = _volume.priority;
        _volume.priority = 99;
        if (bloom)
            StartCoroutine(IIncreaseBloomValue());
        StartCoroutine(IIncreaseVignetteValue());
        if (bloom)
        {
            while (bloom.intensity.value < bloomIntensity)
            {
                yield return null;
            }
        }
        else
        {
            while (vignette.intensity.value < 1)
            {
                yield return null;
            }
        }
        isDarkened = true;
        isBrightened = false;
        _volume.priority = priority;
        yield return null;
    }
    private IEnumerator IIncreaseVignetteValue()
    {
        while(vignette.intensity.value < 1)
        {
            vignette.intensity.value += _fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator IDecreaseVignetteValue()
    {
        while (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= _fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator IIncreaseBloomValue()
    {
        while (bloom.intensity.value < bloomIntensity)
        {
            bloom.intensity.value += _bloomFadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator IDecreaseBloomValue()
    {
        while (bloom.intensity.value > 0)
        {
            bloom.intensity.value -= _bloomFadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator IBrightenArea()
    {
        if (bloom)
            StartCoroutine(IDecreaseBloomValue());
        StartCoroutine(IDecreaseVignetteValue());
        if (bloom)
        {
            while (bloom.intensity.value > 0)
            {
                yield return null;
            }
        }
        else
        {
            while (vignette.intensity.value > 0)
            {
                yield return null;
            }
        }
        if (_volume.enabled)
            _volume.enabled = false;
        isBrightened = true;
        isDarkened = false;
        yield return null;
    }
}
