using UnityEngine;
using System.Collections;

public class SpitterAttackState : SpitterBaseState
{
    private float rangedAttackDistance = 110.0f; // The distance for ranged attacks
    private float attackTime = 2.0f; // Time the attack animation should play
    public float attackCooldown = 2.0f;
    // private float lastAttackTime = .0f;
    private bool isAttackInitiated = false; // To control attack initiation



    private Animator animator;
    private Transform playerTransform;
    private SpitterStateManager spitter;

    public override void EnterState(SpitterStateManager spitter)
    {
        this.spitter = spitter;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = spitter.GetComponent<Animator>();

        // Start the attack animation and projectile firing simultaneously
        isAttackInitiated = false; // Reset attack initiation flag

        animator.SetBool("isAttacking", true);
        // RangedAttack();

        Debug.Log("[SpitterAttackState] Attack routine initiated.");

        // Start a coroutine to end the attack after the animation duration
        // spitter.StartCoroutine(EndAttack());
    }

    public override void UpdateState(SpitterStateManager spitter)
    {

        float distanceToPlayer = Vector3.Distance(spitter.transform.position, playerTransform.position);
        Debug.Log($"[SpitterAttackState] Distance to player: {distanceToPlayer}");

        // Start the attack routine only if not already initiated and player is in range
        if (distanceToPlayer <= rangedAttackDistance && !isAttackInitiated)
        {
            isAttackInitiated = true; // Set the flag to true to prevent re-initiation
            spitter.StartCoroutine(AttackRoutine());
        }

        // If the player moves out of attack range, consider ending the attack early
        else if (distanceToPlayer > rangedAttackDistance && isAttackInitiated)
        {
            // Stop the attack coroutine and switch state to idle
            isAttackInitiated = false;
            animator.SetBool("isAttacking", false);

            spitter.StopCoroutine(AttackRoutine()); // Stop the current attack routine
            spitter.SwitchState(spitter.IdleState);
            Debug.Log("[SpitterAttackState] Player out of attack range. Ending attack early.");
        }
    }



    // Use this method to end the attack animation and switch state after a fixed time
    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackTime);
        RangedAttack();

        animator.SetBool("isAttacking", false);
        spitter.SwitchState(spitter.IdleState);
        Debug.Log("[SpitterAttackState] Attack routine complete. Switching to idle state.");
    }


    // This method will fire the projectile immediately
    private void RangedAttack()
    {
        GameObject projectilePrefab = spitter.ProjectilePrefab; // Retrieve from state manager
        Transform projectileSpawnPoint = spitter.ProjectileSpawnPoint; 

        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectileObject = GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();

            // Calculate the direction from the spitter to the player
            Vector2 direction = (playerTransform.position - projectileSpawnPoint.position).normalized;
            projectile.setDirection(direction);

            Debug.Log("[SpitterAttackState] Projectile fired.");
        }
        else
        {
            Debug.LogError("[SpitterAttackState] Projectile prefab or spawn point not assigned in SpitterStateManager.");
        }
    }


    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {


    }

    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other)
    {

    }
}
