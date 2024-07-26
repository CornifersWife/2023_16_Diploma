
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DialogueState : BaseState {
    private MovingSM movingSM;
    private NavMeshAgent navMeshAgent;
    
    public DialogueState(MovingSM stateMachine) : base("Dialogue", stateMachine) {
        movingSM = stateMachine;
    }

    public override void Enter() {
        base.Enter();
        navMeshAgent = movingSM.GetNavMeshAgent();
        navMeshAgent.isStopped = true;
        navMeshAgent.updateRotation = false;
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        
        if (DialogueController.Instance.DialogueClosed) {
            movingSM.IsDialogue = false;
            movingSM.ChangeState(movingSM.idleState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        FacePlayer();
    }

    public override void Exit() {
        base.Exit();
    }

    private void FacePlayer() {
        Transform target = movingSM.GetPlayer().transform;
        Transform npc = navMeshAgent.transform;
        
        Vector3 lookPos = (target.position - npc.position).normalized;
        lookPos.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(lookPos);
        npc.rotation = Quaternion.Slerp(npc.rotation, lookRotation, Time.deltaTime * navMeshAgent.speed);
    }
}