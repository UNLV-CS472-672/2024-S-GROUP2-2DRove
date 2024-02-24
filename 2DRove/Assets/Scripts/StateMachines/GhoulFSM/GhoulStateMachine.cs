using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulStateMachine : StateManager<GhoulStateMachine.EGhoulState>
{
    public enum EGhoulState
    {
        Wake,
        Walk,
        Attack,
        Hit,
        Death,
        Spawn
    }
    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        CurrentState = States[EGhoulState.Spawn];
        CurrentState.EnterState();
    }

    public void Initialize()
    {
        States.Add(EGhoulState.Spawn, new GhoulSpawnState(EGhoulState.Spawn));
        States.Add(EGhoulState.Wake, new GhoulWakeState(EGhoulState.Wake));
        States.Add(EGhoulState.Walk, new GhoulWalkState(EGhoulState.Walk));
    }
}
