using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulWalkState : BaseState<GhoulStateMachine.EGhoulState>
{
    private Transform player;
   
    public GhoulWalkState(GhoulStateMachine.EGhoulState EState) : base(EState)
    {}

    public override void EnterState()
    {
        Debug.Log("Entering walk state");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        updateID();
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        Vector2 Direction = (player.position - self.position).normalized;
        rb.AddForce(Direction * 1f);
        m_Animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        bool flipped = rb.velocity.x < 0;
        self.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    public override GhoulStateMachine.EGhoulState GetNextState()
    {
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
