public class IdleState : BaseState {
    public IdleState(MovingSM stateMachine) : base("Idle", stateMachine) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        stateMachine.ChangeState(((MovingSM) stateMachine).waitingState);
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }

    public override void Exit() {
        base.Exit();
    }
}
