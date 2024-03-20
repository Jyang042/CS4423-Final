using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public Transform attackPoint3;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public Vector2 Knockback;
    private float attackCooldown = 3f;
    private int attackCounter = 0;
    private float resetCounter = 1f;

    // Reference to the Player script for accessing facing direction
    public Player player;
    private int currentHealth;

    void Start()
    {
        // Find and store the Player script reference
        player = GetComponent<Player>();
        currentHealth = player.health;
    }

    void Update()
    {
        HandleFacingDirection();

        // Check for attack input
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(PerformCombo());
        }
    }

    void HandleFacingDirection()
    {
        if (attackPoint1 != null && attackPoint2 != null && attackPoint3 != null && player != null)
        {
            float horizontalDirection = player.lastHorizontalVector;

            // Flip the attack points based on the facing direction
            FlipAttackPoint(attackPoint1, horizontalDirection);
            FlipAttackPoint(attackPoint2, horizontalDirection);
            FlipAttackPoint(attackPoint3, horizontalDirection);
        }
    }

    void FlipAttackPoint(Transform attackPoint, float horizontalDirection)
    {
        // Set the attack point's local scale based on the facing direction
        Vector3 attackPointScale = attackPoint.localScale;
        attackPointScale.x = horizontalDirection > 0 ? Mathf.Abs(attackPointScale.x) : -Mathf.Abs(attackPointScale.x);
        attackPoint.localScale = attackPointScale;

        // Move the attack point to the left when facing left
        Vector3 attackPointPosition = attackPoint.localPosition;
        attackPointPosition.x = horizontalDirection > 0 ? Mathf.Abs(attackPointPosition.x) : -Mathf.Abs(attackPointPosition.x);
        attackPoint.localPosition = attackPointPosition;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        //Play hurt animation
        animator.SetTrigger("Hurt");
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("You Died");
        //Die Aniamtion
        animator.SetBool("IsDead", true);
        //Game Over Screen

    }

    void Attack1()
    {
        // Play attack Animation
        animator.SetTrigger("Attack1");

        // Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            // Debug.Log("Enemy hit " + enemy.name);
        }
    }

    void Attack2()
    {
        // Play attack Animation
        animator.SetTrigger("Attack2");

        // Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            // Debug.Log("Enemy hit " + enemy.name);
        }
    }

    void Attack3()
    {
        // Play attack Animation
        animator.SetTrigger("Attack3");

        // Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint3.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            // Debug.Log("Enemy hit " + enemy.name);
        }
    }

    private IEnumerator PerformCombo()
    {
        if(attackCounter == 0)
        {
            Attack1();
            attackCounter++;
        }
        else if(attackCounter == 1)
        {
            Attack2();
            attackCounter++;

        }
        else if(attackCounter ==2)
        {
            Attack3();
            attackCounter = 0;
        }

        // Reset the counter and cooldown
        resetCounter = Time.time + 3f;
        attackCooldown = 3f;

        //Wait for the attack animation to finish before starting next attack
        yield return new WaitForSeconds(attackCooldown);

         // Update the attack cooldown
        while (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
            yield return null;
        }

        // Reset the counter if the cooldown is finished
        attackCounter = 0;
        animator.SetBool("nextAttack", false);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint1 == null)
            return;
        Gizmos.DrawWireSphere(attackPoint1.position, attackRange);
    }

}
