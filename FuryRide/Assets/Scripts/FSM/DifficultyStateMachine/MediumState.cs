public class MediumState : DifficultyState
{
    public MediumState(DifficultyStateMachine.DificultyStage key, DifficultyStateMachine stateMachine, DifficultyStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {
        GoldToAddWhenWaveDone = 10;
    }
}