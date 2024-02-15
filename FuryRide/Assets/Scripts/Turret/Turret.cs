using System;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject _objectToRotate;
    [SerializeField] private GameObject _cameraRotation;
    [SerializeField] private TurretFindTarget _findTarget;

    private GameObject _closestEnemy;

    private void LateUpdate()
    {
        TurretRotating();

    }

    private void TurretRotating()
    {
        _objectToRotate.transform.rotation = _cameraRotation.transform.rotation;
        _closestEnemy = null;
    }

}
