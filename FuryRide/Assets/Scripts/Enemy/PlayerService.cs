using System.Collections;
using UnityEngine;
using Zenject;


public class PlayerService : MonoBehaviour
{
    private EventBus _eventBus;

    [Inject]
    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Start()
    {
        StartCoroutine(SendPlayerPositionSignalCoroutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SendPlayerPositionSignalCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _eventBus.Invoke(new PlayerLocationSignal(transform.position));
        }
    }
}