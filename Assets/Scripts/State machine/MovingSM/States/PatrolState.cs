public class PatrolState : BaseState {
    protected MovingSM movingSM;
    
    public PatrolState(string name, MovingSM stateMachine): base(name, stateMachine) {
        movingSM = stateMachine;
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if(movingSM.IsDialogue())
            movingSM.ChangeState(movingSM.dialogueState);
    }

    public override void Exit() {
        base.Exit();
    }
}