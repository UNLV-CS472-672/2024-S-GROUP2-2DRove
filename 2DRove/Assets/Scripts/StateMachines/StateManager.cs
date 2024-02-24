using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary <EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();
        // Debug.Log(CurrentState.StateKey);
        // Debug.Log(nextStateKey);
        if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if (!IsTransitioningState)
        {
            TransitionToState(nextStateKey);
        }
    }

    public void TransitionToState(EState statekey)
    {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[statekey];
        foreach (KeyValuePair<EState, BaseState<EState>> key in States)
        {
        Debug.Log(key.Key + " " + key.Value);
        }
        CurrentState.EnterState();
        IsTransitioningState = false;
    }

    void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }
    
    void OnTriggerExit(Collider other) 
    {
        CurrentState.OnTriggerExit(other);
    }
}
