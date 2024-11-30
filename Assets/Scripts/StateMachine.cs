using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour
{
    public abstract class State
    {
        public virtual void OnEnter(StateMachine stateMachine) { }
        public virtual void OnExit(StateMachine stateMachine) { }
        public virtual void UpdateState(StateMachine stateMachine) { }
    }

    State currState;
    public State CurrentState { get { return currState; } }

    public void UpdateState()
    {
        currState.UpdateState(this);
    }

    public void ChangeState(State newState)
    {
        if (currState != null)
        {
            currState.OnExit(this);
        }
        currState = newState;
        currState.OnEnter(this);
    }
}
