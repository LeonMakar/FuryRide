using System;
using System.Collections.Generic;
using UnityEngine;

public class CarMoovement : MonoBehaviour
{
    [SerializeField] private float _maxAcceleration = 30f;
    [SerializeField] private float _brackAcceleration = 50f;
    [SerializeField] private float _turnSensivity = 1f;
    [SerializeField] private float _maxSteerAngle = 30f;


    [SerializeField] private List<Wheel> _wheels = new List<Wheel>();
    [SerializeField] private Rigidbody _carRigidbody;
    public Vector3 CenterOfMass;
    private float _mooveInput;
    private float _steerInput;

    public enum WheelSide
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject WheelModel;
        public WheelCollider WheelCollider;
        public WheelSide WheelSide;
    }

    private void Start()
    {
        _carRigidbody.centerOfMass = CenterOfMass;
    }
    private void GetInputs()
    {
        _mooveInput = Input.GetAxis("Vertical");
        _steerInput = Input.GetAxis("Horizontal");
    }


    private void FixedUpdate()
    {
        Moove();
        Stear();
    }
    private void Update()
    {
        GetInputs();
        AnimateWhells();
        Break();
    }
    private void Moove()
    {
        foreach (var wheel in _wheels)
        {
            wheel.WheelCollider.motorTorque = _mooveInput * 600 * _maxAcceleration * Time.deltaTime;
        }
    }

    private void Stear()
    {
        foreach (var wheel in _wheels)
        {
            if (wheel.WheelSide == WheelSide.Front)
            {
                var stearAngle = _steerInput * _turnSensivity * _maxSteerAngle;
                wheel.WheelCollider.steerAngle = Mathf.Lerp(wheel.WheelCollider.steerAngle, stearAngle, 0.6f);
            }
        }
    }
    private void AnimateWhells()
    {
        foreach (var wheel in _wheels)
        {
            wheel.WheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            wheel.WheelModel.transform.position = position;
            wheel.WheelModel.transform.rotation = rotation;
        }
    }

    private void Break()
    {
        if (Input.GetKey(KeyCode.Space))
            foreach (var wheel in _wheels)
                wheel.WheelCollider.brakeTorque = _brackAcceleration * 300 * Time.deltaTime;
        else
            foreach (var wheel in _wheels)
                wheel.WheelCollider.brakeTorque = 0;
    }
}
