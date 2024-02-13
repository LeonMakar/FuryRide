using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    public Vector3 LookPosition;


    private void FixedUpdate()
    {
        Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = _mainCamera.ScreenPointToRay(centerScreen);

        if (Physics.Raycast(ray, out RaycastHit hit))
            LookPosition = hit.point;
    }
}
