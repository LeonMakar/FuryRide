public class SuperEasyState : DifficultyState
{
    public SuperEasyState(DifficultyStateMachine.DificultyStage key, DifficultyStateMachine stateMachine, DifficultyStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {
        GoldToAddWhenWaveDone = 5;
    }
}