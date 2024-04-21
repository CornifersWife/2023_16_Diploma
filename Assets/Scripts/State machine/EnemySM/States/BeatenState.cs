public class BeatenState : BaseState {
    private EnemySM enemySM;
    public BeatenState(EnemySM stateMachine) : base("Beaten", stateMachine) {
        enemySM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
        enemySM.GetRenderer().material = enemySM.GetBeatenMaterial();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        //keep this state
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}
