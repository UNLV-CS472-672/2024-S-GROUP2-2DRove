using UnityEngine;
using UnityEngine.UIElements;

public class SpitterIdleState : SpitterBaseState
{
    // Start is called before the first frame update
    private bool idling = true;
    private Transform player;

    public override void EnterState(SpitterStateManager spitter)
    {
        Debug.Log("Entering Wake State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(SpitterStateManager spitter)
    {
        
        spitter.SwitchState(spitter.AggroState);
    }

    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(SpitterStateManager ghoul, Collider2D other) {
    }


}
