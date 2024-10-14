using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOffLightsAndTurnOnHeadlampProperty : ScriptableActionInvoker
{
    [SerializeField]
    private VignetteHandler _vignetteHandler;
    [SerializeField]
    private AudioSource _headlampSwitchAudio;
    [SerializeField]
    private float _interactionTime, _waitAfterDarkenArea, _waitForBrightenArea;
    [SerializeField]
    private AudioClip _toggleHeadlampOff, _toggleHeadlampOn;
    public void InvokeFinishEvent()
    {
        StartCoroutine(IInvokeAction());
    }
    public override IEnumerator IInvokeAction()
    {
        _vignetteHandler.SetVolumePriority(99);
        _vignetteHandler.DarkenArea();
        _vignetteHandler.ToggleVignette(true);
        yield return new WaitForSeconds(_waitAfterDarkenArea);
        _headlampSwitchAudio.PlayOneShot(_toggleHeadlampOn);
        _vignetteHandler.vignette.center.value = Vector2.one * 0.5f;
        yield return new WaitForSeconds(_interactionTime);
        _vignetteHandler.BrightenArea(true);
        _headlampSwitchAudio.PlayOneShot(_toggleHeadlampOff);
        yield return new WaitForSeconds(_waitForBrightenArea);
        _vignetteHandler.ToggleVignette(false);
        _vignetteHandler.SetVolumePriority(0);
        yield return null;
    }
}
