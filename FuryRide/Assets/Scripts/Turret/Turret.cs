using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject _objectToRotate;
    [SerializeField] private GameObject _cameraRotation;
    [SerializeField] GameObject _rawStartPosition;
    [SerializeField] GameObject _bulletStartPosition;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] GameObject _trailObject;
    [SerializeField] private float _trailSpeed;
    [SerializeField] private float _fireRate;
    private bool _canFire = true;
    private TrailPool _trailPool;
    private CharacterActions _characterAction;
    private Zombie _zombie;

    [Inject]
    public void Construct(CharacterActions characterAction)
    {
        _characterAction = characterAction;
    }

    private void LateUpdate()
    {
        TurretRotating();

    }

    private void TurretRotating()
    {
        _objectToRotate.transform.rotation = _cameraRotation.transform.rotation;
    }


    private void Awake()
    {
        _trailPool = new TrailPool(20, _trailObject);
    }
    private void FixedUpdate()
    {
        FindTargetForAimPoint();
    }
    private void FindTargetForAimPoint()
    {
        Ray ray = new Ray(_rawStartPosition.transform.position, _rawStartPosition.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Int32.MaxValue))
        {
            if (hit.collider.transform.gameObject.layer == 8 && _canFire)
            {
                StartCoroutine(WaitFireRate(hit.point));
                _canFire = false;
                if (_zombie == null)
                {
                    hit.transform.TryGetComponent(out Zombie zombie);
                    _zombie = zombie;
                    _zombie.OnUnderAim();
                }
                else if (_zombie.gameObject != hit.transform.gameObject)
                {
                    hit.transform.TryGetComponent(out Zombie zombie);
                    _zombie = zombie;
                    _zombie.OnUnderAim();
                }
                else
                {
                    _zombie.OnUnderAim();
                }

            }
            _lineRenderer.SetPosition(0, _rawStartPosition.transform.position);
            _lineRenderer.SetPosition(1, hit.point);
        }
    }
    private IEnumerator WaitFireRate(Vector3 hitPoint)
    {
        StartCoroutine(StartFrowTrail(hitPoint));
        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }

    private IEnumerator StartFrowTrail(Vector3 hitPosition)
    {
        Vector3 startPoint = _bulletStartPosition.transform.position + _rawStartPosition.transform.TransformDirection(Vector3.forward);
        Vector3 finishPoint = hitPosition;
        float distance = Vector3.Distance(startPoint, finishPoint);
        float remeningDistance = distance;
        GameObject traailGameObject = _trailPool.GetFromPool();
        traailGameObject.transform.position = startPoint;
        traailGameObject.transform.rotation = Quaternion.LookRotation(finishPoint - startPoint);
        while (remeningDistance > 0)
        {
            var trailPos = Vector3.Lerp(startPoint, finishPoint, 1 - (remeningDistance / distance));
            traailGameObject.transform.position = trailPos;
            remeningDistance -= _trailSpeed * Time.deltaTime;
            yield return null;
        }
        traailGameObject.SetActive(false);
    }
}
