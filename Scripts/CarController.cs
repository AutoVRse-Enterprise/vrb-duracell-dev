using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class CarController : PhysicsGadgetJoystick
{
    public float maxSpeed = 20f;  // Maximum speed of the car
    public float acceleration = 10f;  // How fast the car accelerates
    public float turnSpeed = 5f;  // How fast the car turns
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private RCCarSFXHandler _sfxHandler;
    private Vector3 _axis;

    void Update()
    {
        _axis  = GetValue();

         Vector3 forwardForce = acceleration * _axis.y * _rigidbody.transform.forward;

        _rigidbody.AddForce(forwardForce, ForceMode.Acceleration);

        float steer = _axis.x * turnSpeed;
        _rigidbody.AddTorque(0f, steer, 0f, ForceMode.VelocityChange);

        if (_rigidbody.velocity.magnitude > maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
        }

    }
}
