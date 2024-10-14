using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using UnityEngine;

public class HandheldRemotePointer : TriggerableEventProperty
{
    [SerializeField]
    private Transform _forwardPointer;
    [SerializeField]
    private float _maxRaycastDistance;
    [SerializeField]
    private PointedObjectIntroducerProperty _pointedObjectIntroducer;
    [SerializeField]
    private OVRAutoHandTracker _ovrLeftHandTracker, _ovrRightHandTracker;
    [SerializeField]
    private GrabbablePoseAnimaion _grabbablePoseAnimaion;
    [SerializeField]
    private AutoHandPlayer _autoHandPlayer;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    [Range(0, 1)]
    private float _raycastThreshold;
    private float thumbCurl;
    private Vector3 _startPoint, _endPoint;
    public bool isPointing = false;
    private void Start()
    {
        _lineRenderer.useWorldSpace = false;
    }
    public float remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }
    private void Update()
    {
        isPointing = false;
        _lineRenderer.enabled = false;
        if (_autoHandPlayer.handLeft.IsHolding())
        {
            thumbCurl = _ovrLeftHandTracker.GetFingerCurl(OVRFingerEnum.thumb);
            
            //print($"THUMBCURL VALUE FOR LEFT HAND: " + thumbCurl);
        }
        else if (_autoHandPlayer.handRight.IsHolding())
        {
            thumbCurl = _ovrRightHandTracker.GetFingerCurl(OVRFingerEnum.thumb);
           // print($"THUMBCURL VALUE FOR RIGHT HAND: " + thumbCurl);
        }
        else
        {
          //  print("NOT GRABBED YET");
        }
        _grabbablePoseAnimaion.customValue =  remap(thumbCurl, 0, 0.5f, 0, 1f);
       // print("GRABBABLE POSE ANIMATION CUSTOM VALUE: " + _grabbablePoseAnimaion.customValue);
        if (_grabbablePoseAnimaion.customValue > _raycastThreshold)
        {
            isPointing = true;
            StartCoroutine(ICastRay());
        }
    }
    public void SetPointedObjectIntroducer(PointedObjectIntroducerProperty pointedObjectIntroducer) 
    {
        _pointedObjectIntroducer = pointedObjectIntroducer;
    }
    public void CastRay()
    {
        StartCoroutine(ICastRay());
    }
    public IEnumerator ICastRay()
    {
        print("INSIDE ICASTRAY!");
        Ray ray = new Ray(_forwardPointer.position, _forwardPointer.forward);
        RaycastHit hit;
        bool hasCollided = Physics.Raycast(ray, out hit, _maxRaycastDistance);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);
        _lineRenderer.enabled = true;
        if (hasCollided)
        {
            print("HIT OBJECT: " + hit.collider.gameObject.name);
            _pointedObjectIntroducer.IntroduceObject(hit.collider.gameObject);
        }
        else
        {
            print("HIT NO OBJECT!");
        }
        yield return null;
    }
}
