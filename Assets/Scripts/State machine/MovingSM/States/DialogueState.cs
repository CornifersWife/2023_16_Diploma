
using UnityEngine;
using UnityEngine.AI;

public class DialogueState : BaseState {
    private MovingSM movingSM;
    
    public DialogueState(MovingSM stateMachine) : base("Dialogue", stateMachine) {
        movingSM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
        NavMeshAgent navMeshAgent = movingSM.GetNavMeshAgent();
        navMeshAgent.isStopped = true;
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (!DialogueManager.Instance.IsOpen && !EnemyPopup.Instance.IsOpen) {
            movingSM.IsDialogue = false;
            movingSM.ChangeState(movingSM.idleState);
        }
    }

    public override void Exit() {
        base.Exit();
    }
}