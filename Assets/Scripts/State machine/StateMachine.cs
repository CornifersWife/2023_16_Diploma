using UnityEngine;

public class StateMachine : MonoBehaviour {
    private BaseState currentState;

    private void Start() {
        currentState = GetInitialState();
    }

    private void Update() {
        if(currentState != null)
            currentState.UpdateLogic();
    }

    private void LateUpdate() {
        if(currentState != null)
            currentState.UpdatePhysics();
    }

    public void ChangeState(BaseState newState) {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState() {
        return null;
    }

    public BaseState GetCurrentState() {
        return currentState;
    }
}
