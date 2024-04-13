using UnityEngine;

public class WaitingState : BaseState {
    private MovingSM movingSM;
    private float waitCounter;
    private float waitTime;

    public WaitingState(MovingSM stateMachine) : base("Waiting", stateMachine) {
        movingSM = stateMachine;
    }

    public override void Enter() {
        waitTime = movingSM.GetWaitTime();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (movingSM.IsWaiting())
            Wait();
        else {
            movingSM.ChangeState(movingSM.walkingState);
        }
    }

    public override void Exit() {
        base.Exit();
        waitCounter = 0f;
    }

    private void Wait() {
        waitCounter += Time.deltaTime;
        if (waitCounter >= waitTime)
            movingSM.SetWaiting(false);
    }
}    