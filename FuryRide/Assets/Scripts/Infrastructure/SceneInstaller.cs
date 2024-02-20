using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private CarMoovement _player;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private BonusePool _bonusPool;
    [SerializeField] private CarsSaveData _carsSaveData;
    [SerializeField] private UIMoneyShower _moneyShower;
    [SerializeField] private GameStateMachine _gameStateMachine;
    [SerializeField] private SaveService _saveService;
    

    public override void InstallBindings()
    {
        BindInputAction();
        BindEventBus();
        BindPlayer();
        BindHealthBar();
        BindEnemyPool();
        BindBonusPool();
        BindEnemyFactory();
        BindWeaponSaveData();
        BindMoneyShower();
        BindGameStateMachine();
        BindSaveAndLoadPrecess();
        BindCharacterAction();
        BindInstallerInterfeces();
    }

    private void BindInstallerInterfeces()
    {
        Container
            .BindInterfacesTo<SceneInstaller>()
            .FromInstance(this)
            .AsSingle();
    }

    private void BindCharacterAction()
    {
        Container.Bind<CharacterActions>()
                    .AsSingle()
                    .NonLazy();
    }

    private void BindBonusPool()
    {
        Container
            .Bind<BonusePool>()
            .FromInstance(_bonusPool)
            .AsSingle()
            .NonLazy();
    }

    private void BindSaveAndLoadPrecess()
    {
        Container
            .Bind<SaveService>()
            .FromInstance(_saveService)
            .AsSingle()
            .NonLazy();
    }

    private void BindGameStateMachine()
    {
        Container
            .Bind<GameStateMachine>()
            .FromInstance(_gameStateMachine)
            .AsSingle()
            .NonLazy();
    }
    private void BindMoneyShower()
    {
        Container
            .Bind<UIMoneyShower>()
            .FromInstance(_moneyShower)
            .AsSingle();
    }
    private void BindWeaponSaveData()
    {
        Container
            .Bind<CarsSaveData>()
            .FromInstance(_carsSaveData)
            .AsSingle();
    }
    private void BindEnemyFactory()
    {
        Container
            .Bind<IFactory>()
            .To<EnemyFactory>()
            .AsSingle();
    }
    private void BindEnemyPool()
    {
        Container.Bind<EnemyPool>().FromInstance(_enemyPool).AsSingle().NonLazy();
    }
    private void BindHealthBar()
    {
        Container.Bind<HealthBar>().FromInstance(_healthBar).AsSingle().NonLazy();
    }

    private void BindPlayer()
    {
        Container.Bind<CarMoovement>()
                    .FromInstance(_player)
                    .AsSingle().NonLazy();
    }
    private void BindEventBus()
    {
        Container.Bind<EventBus>().AsSingle().NonLazy();
    }
    private void BindInputAction()
    {
        //Container.Bind<CharacterActions>().FromNew()
            //.AsSingle().NonLazy();
    }

    public void Initialize()
    {
        _bonusPool.Initialize();
        _enemyPool.Initialize(Container.Resolve<IFactory>());
        Debug.Log("Чек");
    }
}
