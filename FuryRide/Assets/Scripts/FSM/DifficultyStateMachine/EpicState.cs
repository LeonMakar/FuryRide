public class EpicState : DifficultyState
{
    public EpicState(DifficultyStateMachine.DificultyStage key, DifficultyStateMachine stateMachine, DifficultyStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {
        GoldToAddWhenWaveDone = 30;
    }
}