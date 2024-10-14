using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringObjectsInCameraView : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _untouchedObjects;
    [SerializeField]
    private GameObject _cameraEyeAnchor;
    [SerializeField]
    private Vector3 _forwardLocation;
    [SerializeField]
    private float _forwardDistance;
    private void Start()
    {
        
    }
    public void SetObjectsInfrontOfCamera()
    {
        _forwardLocation = _cameraEyeAnchor.transform.forward * _forwardDistance;
        

    }
}
