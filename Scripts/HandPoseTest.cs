using System.Collections;
using System.Collections.Generic;
using Autohand.Demo;
using UnityEngine;

public class HandPoseTest : MonoBehaviour
{
    [SerializeField]
    private OVRAutoHandTracker _oVRAutoHandTracker;

    [SerializeField]
    private GameObject _cube;
    [SerializeField]
    private OVRFingerEnum _thumb, _indexfinger, _middleFinger, _ringFinger, _pinky;
    private float _thumbCurl, _indexFingerCurl, _middleFingerCurl, _ringFingerCurl, _pinkyCurl;

    private void Update()
    {
        _thumbCurl =  _oVRAutoHandTracker.GetFingerCurl(_thumb);
        _indexFingerCurl =  _oVRAutoHandTracker.GetFingerCurl(_indexfinger);
        _middleFingerCurl =  _oVRAutoHandTracker.GetFingerCurl(_middleFinger);
        _ringFingerCurl =  _oVRAutoHandTracker.GetFingerCurl(_ringFinger);
        _pinkyCurl =  _oVRAutoHandTracker.GetFingerCurl(_pinky);
        if (_thumbCurl > 0.4f && _indexFingerCurl < 0.1f && _middleFingerCurl > 0.7f && _ringFingerCurl > 0.7f && _pinkyCurl > 0.7f)
        {
            _cube.SetActive(true);
        }
        else
        {
            _cube.SetActive(false);
        }


    }
}
