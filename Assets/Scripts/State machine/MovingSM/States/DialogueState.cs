
using UnityEngine.AI;

public class DialogueState : BaseState {
    private MovingSM movingSM;
    
    public DialogueState(MovingSM stateMachine) : base("Dialogue", stateMachine) {
        movingSM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
        NavMeshAgent navMeshAgent = movingSM.GetNavMeshAgent();
        navMeshAgent.SetDestination(navMeshAgent.transform.position);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if(!movingSM.IsDialogue())
            movingSM.ChangeState(movingSM.idleState);
    }

    public override void Exit() {
        base.Exit();
    }
}