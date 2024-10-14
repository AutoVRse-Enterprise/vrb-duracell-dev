using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class GrabbableMomentSpecificInitializer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Grabbable>().onGrab.AddListener(OnGrabbed);
    }
    public void OnGrabbed(Hand hand, Grabbable grabbable)
    {
        if (!TryGetComponent<GrabLock>(out _))
        {
            gameObject.AddComponent<GrabLock>();
        }
    }
}
