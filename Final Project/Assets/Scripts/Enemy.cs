using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health = 100;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] int attackDamage = 10;
    [SerializeField] float attackCooldown = 5f;
    private float lastAttackTime;
    private float currentHealth;
    Animator animator;
    public Transform attackPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
        lastAttackTime = -attackCooldown; // Set last attack time to a value before Time.time
    }

    void Update()
    {
        // Check if canAttack
        if (CanAttack())
        {
            Attack();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        // Die Animation
        animator.SetBool("IsDead", true);
        // Disabling enemy's behavior
        this.enabled = false;
        // Destroy the GameObject after some time
        Destroy(gameObject, 3f);
    }

    private bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Detect if the player is within attack range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D playerCollider in hitPlayer)
        {
            if (playerCollider.CompareTag("Player"))
            {
                // Deal damage to the player
                PlayerCombat playerCombat = playerCollider.GetComponent<PlayerCombat>();
                if (playerCombat != null)
                {
                    playerCombat.TakeDamage(attackDamage);
                }
                // Update attack time
                lastAttackTime = Time.time;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
