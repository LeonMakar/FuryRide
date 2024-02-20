using Cinemachine;
using System.Collections;
using UnityEngine;

public class MenuChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cameraStart;
    [SerializeField] private CinemachineVirtualCamera _cameraFinish;
    [SerializeField] private Canvas _canvasToDisable;
    [SerializeField] private Canvas _canvasToEnable;
    [SerializeField] private float _cameraTransitionDuration = 2f;

    public void ChangeMenu()
    {
        _cameraStart.Priority--;
        _cameraFinish.Priority = 15;

        StartCoroutine(WaitCameraChangeTransition(_cameraTransitionDuration));
    }

    public void StartGame()
    {
        _cameraStart.Priority--;
        _cameraFinish.Priority = 15;
        StartCoroutine(WaitCameraChangeTransition(_cameraTransitionDuration));
    }

    private IEnumerator WaitCameraChangeTransition(float transitionDuration)
    {
        if (_canvasToDisable != null)
            _canvasToDisable.enabled = false;
        yield return new WaitForSeconds(transitionDuration);
        if (_canvasToEnable != null)
            _canvasToEnable.enabled = true;
    }
}
