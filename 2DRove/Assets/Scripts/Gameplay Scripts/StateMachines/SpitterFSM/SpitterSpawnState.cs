using UnityEngine;

public class SpitterSpawnState : SpitterBaseState
{
    // Start is called before the first frame update
    private float spawnTime = 1f;
    public override void EnterState(SpitterStateManager spitter){
        Debug.Log("Spitter Entering Spawn State...");
    }
    public override void UpdateState(SpitterStateManager spitter){
         if (spawnTime<= 0 ){
            spitter.SwitchState(spitter.IdleState);
         }
         spawnTime -= Time.deltaTime;
    }
    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other){
    
    }
    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other){
        Debug.Log(other.tag); //
        if(other.tag == "Player"){
            spitter.SwitchState(spitter.AttackState);
        }
    }

    public override void EventTrigger(SpitterStateManager spitter)
    {

    }

    public override void TakeDamage(SpitterStateManager spitter)
    {
        spitter.SwitchState(spitter.HitState);
    }
}
