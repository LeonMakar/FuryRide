public class DifficultyState : BaseState<DifficultyStateMachine.DificultyStage>
{
    private EnemySpawner _enemySpawner;
    private BonusSpawner _bonusSpawner;
    private EnemySpawnChanceData _enemyChanceData;
    private BonusSpawnChanceData _bonusChanceData;
    private DifficultyStateMachine.DificultyStage _stateToTransit;
    private int _transititonValue;
    private DifficultyStateMachine _stateMachine;
    protected int GoldToAddWhenWaveDone;
    public DifficultyState(DifficultyStateMachine.DificultyStage key, DifficultyStateMachine stateMachine, DifficultyStateMachine.DificultyStage stateToTransit) : base(key)
    {
        _stateMachine = stateMachine;
        _transititonValue = stateMachine.TransitionValueFromState[(int)key];
        _enemySpawner = stateMachine.ZombieSpawner;
        _bonusSpawner = stateMachine.BonusSpawner;
        _enemyChanceData = stateMachine.EnemyChanceData[(int)key];
        _bonusChanceData = stateMachine.BonusChanceDatas[(int)key];
        _stateToTransit = stateToTransit;

    }

    public override void EnterToState()
    {
        _enemySpawner.ChangeEnemySpawnData(_enemyChanceData);
        _bonusSpawner.ChangeBonusSpawnData(_bonusChanceData);
    }

    public override void ExitFromState()
    {
        _stateMachine.GoldToAdd = GoldToAddWhenWaveDone;
        _stateMachine.StartWaveCompliteRewarding();
    }

    public override void UpdateState()
    {
        if (GameConstans.DiffcultyValue >= _transititonValue)
            ChangeStateAction.Invoke(_stateToTransit);
    }
}