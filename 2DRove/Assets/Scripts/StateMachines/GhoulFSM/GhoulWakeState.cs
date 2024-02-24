using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulWakeState : BaseState<GhoulStateMachine.EGhoulState>
{
    public bool idling = true;
    public GhoulWakeState(GhoulStateMachine.EGhoulState EState) : base(EState)
    {}
    
    public override void EnterState()
    {
        Debug.Log("Entering wake state");
    }

    public override void ExitState()
    {
        idling = false;
    }

    public override void UpdateState()
    {
        //Here is where it should look for the player
        //we could have it be within a certain radius, or just have him just run straight towards him, meaning we just skip the wake state and go straight to the walk state
            //if we do a radius, remain in wake state until player is found
        //for now, debug purposes, walks towards player immediately
        ExitState();
    }

    public override GhoulStateMachine.EGhoulState GetNextState()
    {
        if (idling)
            return GhoulStateMachine.EGhoulState.Wake;
        else
            return GhoulStateMachine.EGhoulState.Walk;
    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }
    public override void OnTriggerStay(Collider other)
    {

    }
    public override void OnTriggerExit(Collider other)
    {

    }
}
