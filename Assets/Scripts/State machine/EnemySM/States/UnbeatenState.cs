
public class UnbeatenState : BaseState {
    private EnemySM enemySM;
    
    public UnbeatenState(EnemySM stateMachine) : base("Unbeaten", stateMachine) {
        enemySM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
        //prepare deck?
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        //check if beaten
        if(enemySM.IsBeaten())
            enemySM.ChangeState(enemySM.beatenState);
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }

    public override void Exit() {
        base.Exit();
    }
}
