using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ColliderEventsGetter : MonoBehaviour
{
    public UnityEvent<Transform, Collider> onTriggerEnter, onTriggerExit, onTriggerStay;
    public UnityEvent<Transform, Collision> onCollisionEnter, onCollisionExit, onCollisionStay;

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter?.Invoke(transform, collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        onCollisionStay?.Invoke(transform, collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        onCollisionExit?.Invoke(transform, collision);
    }
    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(transform, other);
    }
    private void OnTriggerStay(Collider other)
    {
        onTriggerStay?.Invoke(transform, other);

    }
    private void OnTriggerExit(Collider other)
    {
        onTriggerExit?.Invoke(transform, other);

    }
}
