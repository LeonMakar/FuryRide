public class UnrealState : DifficultyState
{
    public UnrealState(DifficultyStateMachine.DificultyStage key, DifficultyStateMachine stateMachine, DifficultyStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {
        GoldToAddWhenWaveDone = 50;
    }
}