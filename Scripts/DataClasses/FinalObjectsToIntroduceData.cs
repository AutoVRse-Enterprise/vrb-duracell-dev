using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FinalObjectsToIntroduceData
{
    public GameObject pivotObject;
    public GameObject objectUI;
    public GameObject nearCameraTeleportPos;
    [HideInInspector]
    public Vector3 originalPosition;
    [HideInInspector]
    public Quaternion originalRotation;
    [HideInInspector]
    public Vector3 originalScale;
}
