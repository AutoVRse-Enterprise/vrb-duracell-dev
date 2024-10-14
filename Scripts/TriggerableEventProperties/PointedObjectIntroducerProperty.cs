using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRseBuilder.Core.NoCode;

public class PointedObjectIntroducerProperty : ScriptableActionInvoker
{
    [SerializeField]
    private List<PointedObjectData> _pointedObjectsData;
    [SerializeField]
    private VignetteHandler _vignetteHandler;
    [SerializeField]
    private AudioSource _introductionVoiceOverSource;
    [SerializeField]
    private int _highlightLayer, _normalLayer;
    [SerializeField]
    private PoweredObjectsListHandler _objectsListHandler;
    [SerializeField]
    private ObjectMaterializerNearCamera _objectMaterializer;
    private Coroutine _introduceObjectCoroutine;
    private GameObject _objectPivot = null;
    private bool _hasActionStarted = false;
    [Header("Debug")]
    public GameObject _testObj;
    public bool isDebugging = false;

    private void Start()
    {
        isDebugging = false;
    }
    public override void InvokeAction()
    {
        _hasActionStarted = true;
        GrabbedHandheldPointerHandler.instance.SetPointedIntroducerProperty(this);
    }
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
    public void IntroduceObject(GameObject obj)
    {
        
        PointedObjectData pointedObjectData = GetPointedObjectData(obj);
       // print("INSIDE INTRODUCE OBJECT. POINTED OBJECT IS: " + pointedObjectData != null? pointedObjectData.pointedObject: "not in list");
        if (_introduceObjectCoroutine == null && pointedObjectData != null)
        {
            if (_hasActionStarted || isDebugging )
            {
                if (obj.transform.parent != null)
                {
                    if (obj.transform.parent.CompareTag("ObjectPivot"))
                    {
                        _objectPivot = obj.transform.parent.gameObject;
                    }
                }
                print("conditions to introduce objects fulfilled.");
                _introduceObjectCoroutine = StartCoroutine(IIntroduceObject(pointedObjectData));
            }
        }
    }
    public PointedObjectData GetPointedObjectData(GameObject obj)
    {
        foreach (PointedObjectData data in _pointedObjectsData)
        {
            if (data.pointedObject == obj)
            {
                return data;
            }
        }
        return null;
    }
    public List<PointedObjectData> GetPointedObjectDataList()
    {
        return _pointedObjectsData;
    }
    public IEnumerator IIntroduceObject(PointedObjectData pointedObjectData)
    {
        _objectMaterializer._isObjectIntroducing = true;
        if (_objectPivot)
        {
            pointedObjectData.pivotObject = _objectPivot;
        }
        else
         {
            pointedObjectData.pivotObject = pointedObjectData.pointedObject;
        }
        Collider col = pointedObjectData.pointedObject.GetComponent<Collider>();
        /*earlier the implementation was we disable collider of whatever object we pointed to.
         it was causing issues in other mechanisms so we disabled point through other means
         we need to  disable joystick's box collider for the put battery interaction so this is the fix for
         now*/
        if (col.name == "JoystickMovable")
        {
            col.enabled = false;
        }
        pointedObjectData.uiOriginalPosition = pointedObjectData.pointedObjectUI.transform.position;
        pointedObjectData.uiOriginalRotation = pointedObjectData.pointedObjectUI.transform.rotation;
        print("Introducing object is: " + pointedObjectData.pointedObject);
        _objectsListHandler.TickMarkPanel(pointedObjectData.pointedObject);
        col.isTrigger = true;
        _vignetteHandler.DarkenArea();
        ToggleObjectVisibilityOverVignette(pointedObjectData.pointedObject, true);
        _objectMaterializer.MaterializeObjectNearCamera(pointedObjectData);
        yield return new WaitForSeconds(_objectMaterializer.appearTime + _objectMaterializer.disappearTime);
        pointedObjectData.pointedObjectUI.transform.parent = pointedObjectData.pivotObject.transform.parent;
        _objectMaterializer.MaterializeUI(pointedObjectData.pointedObjectUI);
        _objectMaterializer.RotateObject(pointedObjectData.pivotObject);
        yield return new WaitForSeconds(_objectMaterializer.uiAppearTime);
        foreach(AudioClip audio in pointedObjectData.pointedObjectVoiceOver)
        {
            _introductionVoiceOverSource.PlayOneShot(audio);
            while (_introductionVoiceOverSource.isPlaying)
            {
                yield return null;
            }
        }
        pointedObjectData.pointedObjectUI.transform.parent = pointedObjectData.pivotObject.transform;
        _objectMaterializer.StopRotatingObject();
        _objectMaterializer._isObjectIntroducing = false;
        _objectMaterializer.MaterializeObjectInOriginalPosition(pointedObjectData.pivotObject);
        _objectMaterializer.DemateralizeUI(pointedObjectData.pointedObjectUI);
        yield return new WaitForSeconds(_objectMaterializer.disappearTime + _objectMaterializer.appearTime);
        pointedObjectData.pointedObjectUI.transform.SetPositionAndRotation(pointedObjectData.uiOriginalPosition, pointedObjectData.uiOriginalRotation);
        _pointedObjectsData.Remove(pointedObjectData);
        _vignetteHandler.BrightenArea();
        while (!_vignetteHandler.isBrightened)
        {
            yield return null;
        }
        ToggleObjectVisibilityOverVignette(pointedObjectData.pointedObject, false);
        _introduceObjectCoroutine = null;
        _objectPivot = null;
        _hasActionStarted = false;
        col.isTrigger = false;
        onActionEnd?.Invoke();
        yield return null;
    }
}
