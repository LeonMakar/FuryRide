using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIsActiveState : BaseState<GameStateMachine.GameStates>
{
    private Canvas _gamePlayCanvas;
    private Spawner _enemySpawner;
    private GameObject _playerGameObject;
    private EventBus _eventBus;
    private GameObject[] _menuZombie;
    private EnemyPool _enemyPool;
    private UIMoneyShower _uiMoney;
    private Spawner _bonusSpawner;
    private BonusePool _bonusPool;
    private AudioSource _music;
    private SaveService _saveService;
    public GameIsActiveState(GameStateMachine.GameStates key, Canvas gamePlayCanvas, Spawner enemySpawner, GameObject playerGameObject,
       EventBus eventBus, GameObject[] menuZombie, EnemyPool enemyPool, UIMoneyShower uiMoney, Spawner bonusSpawner, BonusePool bonusePool, AudioSource musicAudio, SaveService save) : base(key)
    {
        _gamePlayCanvas = gamePlayCanvas;
        _enemySpawner = enemySpawner;
        _playerGameObject = playerGameObject;
        _eventBus = eventBus;
        _menuZombie = menuZombie;
        _enemyPool = enemyPool;
        _uiMoney = uiMoney;
        _bonusSpawner = bonusSpawner;
        _bonusPool = bonusePool;
        _music = musicAudio;
        _saveService = save;
    }
    public override void EnterToState()
    {
        _gamePlayCanvas.enabled = true;
        _uiMoney.ResetGoldAndMoneyINRestart();
        _enemySpawner.SetGameActivity(true);
        _enemySpawner.StartSpawning();
        //_playerGameObject.SetActive(true);
        foreach (var zombie in _menuZombie)
            zombie.SetActive(false);
        _eventBus.Invoke(new StartGameSignal(true));
        _uiMoney.ChangeMoneyText();
        _uiMoney.ChangeGoldText();
        _bonusSpawner.SetGameActivity(true);
        _bonusSpawner.StartSpawning();
        _music.volume = 0.5f;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public override void ExitFromState()
    {
        _gamePlayCanvas.enabled = false;
        _enemySpawner.SetGameActivity(false);
        //_playerGameObject.SetActive(false);
        _eventBus.Invoke(new StartGameSignal(false));
        foreach (var zombie in _menuZombie)
            zombie.SetActive(true);
        _enemyPool.RemooveAllEnemyFromScene();
        _bonusSpawner.SetGameActivity(false);
        _bonusSpawner.StopAllCoroutines();
        _bonusPool.RemooveAllObjectFromScene();
        _music.volume = 1;
        Cursor.lockState = CursorLockMode.None;

    }

    public override void UpdateState()
    {
    }

}
