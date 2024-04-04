

public class BeatenState : BaseState {
    public BeatenState(EnemySM stateMachine) : base("Beaten", stateMachine) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        //keep this state
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }

    public override void Exit() {
        base.Exit();
    }
}
