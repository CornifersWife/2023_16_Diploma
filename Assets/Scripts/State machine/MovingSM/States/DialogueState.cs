
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DialogueState : BaseState {
    private MovingSM movingSM;
    
    public DialogueState(MovingSM stateMachine) : base("Dialogue", stateMachine) {
        movingSM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
        Debug.Log("enter dialogue");
        NavMeshAgent navMeshAgent = movingSM.GetNavMeshAgent();
        Debug.Log(navMeshAgent.gameObject.name);
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