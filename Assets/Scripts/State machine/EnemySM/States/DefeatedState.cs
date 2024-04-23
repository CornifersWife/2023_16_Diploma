public class DefeatedState : BaseState {
    private EnemySM enemySM;
    public DefeatedState(EnemySM stateMachine) : base("Defeated", stateMachine) {
        enemySM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
        enemySM.GetRenderer().material = enemySM.GetDefeatedMaterial();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        //keep this state
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}
