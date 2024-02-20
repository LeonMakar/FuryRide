public class EasyState : DifficultyState
{
    public EasyState(DifficultyStateMachine.DificultyStage key, DifficultyStateMachine stateMachine, DifficultyStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {
        GoldToAddWhenWaveDone = 5;
    }
}