using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhoulSpawnState : BaseState<GhoulStateMachine.EGhoulState>
{
    private float time;
    private bool spawned = false;
    public GhoulSpawnState(GhoulStateMachine.EGhoulState EState) : base(EState)
    {}
    public override void EnterState()
    {
        time = Time.time;
        updateID();
    }

    public override void ExitState()
    {
        spawned = true;
    }
    public override void UpdateState()
    {
        if(time + 1f < Time.time)
        {
            // Debug.Log("finished spawning");
            ExitState();
        }
            
    }
    public override GhoulStateMachine.EGhoulState GetNextState()
    {
        if (spawned)
            return GhoulStateMachine.EGhoulState.Wake;
        else
            return GhoulStateMachine.EGhoulState.Spawn;
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
