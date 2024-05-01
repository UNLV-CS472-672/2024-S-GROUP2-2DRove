using UnityEngine;
using System.Collections;

public class SpitterAttackState : SpitterBaseState
{
    private Animator animator;
    private Transform playerTransform;
    private SpitterStateManager spitter;

    public float attackCooldown = 0.7f; // Cooldown between attacks
    private bool isAttackInitiated = false; // To control attack initiation
    private Coroutine attackCoroutine; // To keep track of the coroutine

    public override void EnterState(SpitterStateManager spitter)
    {
        this.spitter = spitter;
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform; // Use null-conditional to prevent error
        animator = spitter.GetComponent<Animator>();

        if (playerTransform != null)
        {
            isAttackInitiated = false; // Reset attack initiation flag
            animator.SetBool("isAttacking", true);
            Debug.Log("Enter Attack State");
            attackCoroutine = spitter.StartCoroutine(AttackRoutine()); // Store coroutine reference
        }
        else
        {
            spitter.SwitchState(spitter.IdleState);
        }
    }

    public override void UpdateState(SpitterStateManager spitter)
    {
        // Dynamically adjust the ranged attack distance based on the SpitterStateManager
        // rangedAttackDistance = spitter.attackRange;

        // If the player is destroyed during the attack, clean up and exit state
        if (playerTransform == null || playerTransform.gameObject == null)
        {
            CleanupAttack();
            return;
        }

        float distanceToPlayer = Vector3.Distance(spitter.transform.position, playerTransform.position);
        if (!isAttackInitiated && distanceToPlayer <= spitter.attackRange)
        {
            attackCoroutine = spitter.StartCoroutine(AttackRoutine());
        }
        else if (distanceToPlayer > spitter.attackRange && isAttackInitiated)
        {
            CleanupAttack();
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttackInitiated = true;
        yield return new WaitForSeconds(attackCooldown); // Wait for the appropriate time to fire the projectile

        // If player has been destroyed, don't continue
        if (playerTransform == null)
        {
            yield break;
        }

        FireProjectile(); // Proceed to fire the projectile

        yield return new WaitForSeconds(attackCooldown); // Cooldown after firing

        // If player has been destroyed, don't continue
        if (playerTransform == null || playerTransform.gameObject == null)
        {
            CleanupAttack();

            yield break;
        }

        CleanupAttack();
    }

    private void FireProjectile()
    {
        if (playerTransform == null) return; // Extra check to ensure player is not null

        GameObject projectilePrefab = spitter.ProjectilePrefab;
        Transform projectileSpawnPoint = spitter.ProjectileSpawnPoint;

        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            // Calculate the direction from the spitter to the player
            Vector2 direction = (playerTransform.position - projectileSpawnPoint.position).normalized;

            // Check the angle of the direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // If the angle is downwards, do not fire
            if (angle < -45f && angle > -135f)
            {
                Debug.Log("[SpitterAttackState] Prevented firing downwards.");
                return;
            }

            GameObject projectileObject = GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.setDirection(direction);
            Debug.Log("[SpitterAttackState] Projectile fired.");
        }
        else
        {
            Debug.LogError("[SpitterAttackState] Projectile prefab or spawn point not assigned in SpitterStateManager.");
        }
    }

    private void CleanupAttack()
    {
        if (attackCoroutine != null)
        {
            spitter.StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        isAttackInitiated = false;
        animator.SetBool("isAttacking", false);
        spitter.SwitchState(spitter.IdleState);
    }
    private Vector2 CalculateDirection()
    {
        return (playerTransform.position - spitter.ProjectileSpawnPoint.position).normalized;
    }

    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {


    }

    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other)
    {

    }

    //done in animation events
    public override void EventTrigger(SpitterStateManager spitter)
    {
        FireProjectile();
    }

    public override void TakeDamage(SpitterStateManager spitter)
    {
        CleanupAttack();
        spitter.SwitchState(spitter.HitState);
    }
}
