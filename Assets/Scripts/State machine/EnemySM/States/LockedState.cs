using UnityEngine;

public class LockedState : BaseState {
    private EnemySM enemySM;
    public LockedState(EnemySM stateMachine) : base("Locked", stateMachine) {
        enemySM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        //check condition
        if(!enemySM.IsLocked())
            enemySM.ChangeState(enemySM.undefeatedState);
    }

    public override void Exit() {
        base.Exit();
    }
}
