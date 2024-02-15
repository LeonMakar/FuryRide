using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFindTarget : MonoBehaviour
{
    [SerializeField] GameObject _bulletStartPosition;
    [SerializeField] LineRenderer _lineRenderer;
    private bool _canFire = true;

    private void FixedUpdate()
    {
        FindTargetForAimPoint();
    }
    private void FindTargetForAimPoint()
    {
        Ray ray = new Ray(_bulletStartPosition.transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Int32.MaxValue))
        {
            //Debug.DrawRay(_bulletStartPosition.transform.position, transform.forward * 30 - transform.up);
            if (hit.collider.transform.gameObject.layer == 7 && _canFire)
            {
                _canFire = false;
                StartCoroutine(WaitFireRate());
            }
            _lineRenderer.SetPosition(0,_bulletStartPosition.transform.position);
            _lineRenderer.SetPosition(1,hit.point);
        }
    }
    private IEnumerator WaitFireRate()
    {
        yield return new WaitForSeconds(1);
        _canFire = true;
    }

}
