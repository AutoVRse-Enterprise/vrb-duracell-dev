using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IntroduceAllObjectsProperty : ScriptableActionInvoker
{
    [SerializeField]
    private PointedObjectIntroducerProperty _pointedObjectIntroducer;
    [SerializeField]
    private AudioSource _introAllObjectsSource;
    [SerializeField]
    private AudioClip _introAllObjectsClip;
    [SerializeField]
    private VignetteHandler _vignetteHandler;
    [SerializeField]
    private ObjectMaterializerNearCamera _objectMaterializerNearCamera;
    [SerializeField]
    private float _appearTime, _disappearTime;
    [SerializeField]
    private int _highlightLayer = 12, _normalLayer = 0;
    [SerializeField]
    private List<FinalObjectsToIntroduceData> _finalIntroObjects;
    private void ToggleObjectVisibilityOverVignette(GameObject obj, bool val)
    {
        if (!obj.CompareTag("UI"))
        {
            obj.layer = val ? _highlightLayer : _normalLayer;
        }

        foreach (Transform child in obj.transform)
        {

            // Log or perform ay actions with the child GameObject
            Debug.Log("Child GameObject: " + child.gameObject.name);

            // Recursively call this function to go deeper into the hierarchy
            ToggleObjectVisibilityOverVignette(child.gameObject, val);
        }
    }
    public void InvokeFinishEvent()
    {
        StartCoroutine(IInvokeAction());
    }
    public override IEnumerator IInvokeAction()
    {
        _vignetteHandler.vignette.center.value = Vector2.up * 2f;
        _vignetteHandler.DarkenArea();
        while (!_vignetteHandler.isDarkened)
        {
            yield return null;
        }
        foreach(FinalObjectsToIntroduceData data in _finalIntroObjects)
        {
            ToggleObjectVisibilityOverVignette(data.pivotObject, true);
            data.originalPosition = data.pivotObject.transform.position;
            data.originalRotation = data.pivotObject.transform.rotation;
            data.originalScale = data.pivotObject.transform.localScale;
            data.pivotObject.transform.DOScale(Vector3.zero, _disappearTime);
        }
        yield return new WaitForSeconds(_disappearTime);
        foreach (FinalObjectsToIntroduceData data in _finalIntroObjects)
        {
            // some gameobjects are turned off after touch and their grabbable counterparts are turned on, causing these ones to not show up during all object introduction. below two lines is a fix for that
            data.pivotObject.SetActive(true);
            data.pivotObject.transform.GetChild(0).gameObject.SetActive(true);
            data.pivotObject.transform.SetPositionAndRotation(data.nearCameraTeleportPos.transform.position, data.nearCameraTeleportPos.transform.rotation);
            data.objectUI.SetActive(true);
            data.objectUI.transform.DOScale(Vector3.one, _appearTime);
            data.pivotObject.transform.DOScale(data.originalScale, _appearTime);
        }
        yield return new WaitForSeconds(_appearTime);
        BGMHandler.instance.PlayBGM();
        _introAllObjectsSource.PlayOneShot(_introAllObjectsClip);
        while (_introAllObjectsSource.isPlaying)
        {
            yield return null;
        }
        BGMHandler.instance.StopBGM();
        foreach (FinalObjectsToIntroduceData data in _finalIntroObjects)
        {
            data.pivotObject.transform.DOScale(Vector3.zero, _disappearTime);
            data.objectUI.transform.DOScale(Vector3.zero, _disappearTime);
        }
        yield return new WaitForSeconds(_disappearTime);
        foreach (FinalObjectsToIntroduceData data in _finalIntroObjects)
        {
            data.pivotObject.transform.SetPositionAndRotation(data.originalPosition, data.originalRotation);
            data.pivotObject.transform.DOScale(data.originalScale, _appearTime);
            ToggleObjectVisibilityOverVignette(data.pivotObject, false);
        }
        yield return new WaitForSeconds(_appearTime);
        _vignetteHandler.BrightenArea();
        while (!_vignetteHandler.isBrightened)
        {
            yield return null;
        }
    }
}
