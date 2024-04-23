
using UnityEngine;

public class UndefeatedState : BaseState {
    private EnemySM enemySM;
    
    public UndefeatedState(EnemySM stateMachine) : base("Undefeated", stateMachine) {
        enemySM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
        enemySM.GetRenderer().material = enemySM.GetUndefeatedMaterial();
        //prepare deck?
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        //check if beaten
        /*
        if(enemySM.IsBeaten())
            enemySM.ChangeState(enemySM.beatenState);
            */
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }

    public override void Exit() {
        base.Exit();
    }
}
