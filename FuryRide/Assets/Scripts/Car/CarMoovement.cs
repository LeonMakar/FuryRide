using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static CarMoovement;

public class CarMoovement : MonoBehaviour
{
    [SerializeField] private float _maxAcceleration = 30f;
    [SerializeField] private float _brackAcceleration = 50f;
    [SerializeField] private float _turnSensivity = 1f;
    [SerializeField] private float _maxSteerAngle = 30f;
    [field: SerializeField] public Rigidbody CarRigidbody { get; private set; }


    [SerializeField] private List<Wheel> _wheels = new List<Wheel>();
    public Vector3 CenterOfMass;
    private float _mooveInput;
    private float _steerInput;
    private float _mouseInput;
    private CharacterActions _characterActions;

    public enum WheelSide
    {
        Front,
        Rear
    }

    [Inject]
    public void Construct(CharacterActions characterActions)
    {
        _characterActions = characterActions;
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
        CarRigidbody.centerOfMass = CenterOfMass;
        _characterActions.Ground.Enable();
    }
    private void GetInputs()
    {
        _mooveInput = _characterActions.Ground.Mooving.ReadValue<float>();
        _steerInput = _characterActions.Ground.Turning.ReadValue<float>();
    }


    private void FixedUpdate()
    {
        Moove();
        Stear();
        Break();
    }
    private void Update()
    {
        GetInputs();
        AnimateWhells();
        Debug.Log(CarRigidbody.velocity.magnitude);
    }
    private void Moove()
    {
        if (CarRigidbody.velocity.magnitude < 10)
            foreach (var wheel in _wheels)
            {
                wheel.WheelCollider.motorTorque = _mooveInput * 600 * _maxAcceleration * Time.deltaTime;
            }
        else
            foreach (var wheel in _wheels)
            {
                wheel.WheelCollider.motorTorque = 0;
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

    public void Break()
    {
        if (Input.GetKey(KeyCode.Space))
            foreach (var wheel in _wheels)
                wheel.WheelCollider.brakeTorque = _brackAcceleration * 300 * Time.deltaTime;
        else
            foreach (var wheel in _wheels)
                wheel.WheelCollider.brakeTorque = 0;
    }
}
