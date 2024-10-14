using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointedObjectData
{
    public GameObject pointedObject;
    public AudioClip[] pointedObjectVoiceOver;
    public GameObject pointedObjectUI;
    [HideInInspector]
    public GameObject pivotObject;
    public GameObject nearCameraTeleportPos;
    [HideInInspector]
    public Vector3 originalPosition;
    [HideInInspector]
    public Quaternion originalRotation;
    [HideInInspector]
    public Vector3 uiOriginalPosition;
    [HideInInspector]
    public Quaternion uiOriginalRotation;
}
