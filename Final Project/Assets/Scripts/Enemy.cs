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
    private bool isDead = false;
    Animator animator;
    public Transform attackPoint;
    Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
        lastAttackTime = -attackCooldown; // Set last attack time to a value before Time.time
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check if canAttack
        if (CanAttack())
        {
            Attack();
        }

        bool isMoving = rb.velocity.magnitude > 0.1f;

        // Set animation parameter accordingly
        animator.SetBool("IsMoving", isMoving);

    }


    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
            // Apply knockback
            rb.velocity = Vector2.zero; // Reset velocity
            rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);
            //Debug.Log("Applying knockback. Force: " + knockbackForce);
            //Debug.Log("Knockback Direction: " + knockbackDirection.normalized);
        }
    }


    void Die()
    {   
        if (isDead) return; // If already dead, do nothing
        isDead = true;
        animator.SetTrigger("Hurt");
        Debug.Log("Enemy" + this.name + "Died");
        // Die Animation
        animator.SetBool("IsDead", true);
        //Disable Movement
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        //Generate Loot
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        // Disabling enemy's behavior
        this.enabled = false;
        // Destroy the GameObject after some time
        Destroy(gameObject, 3f);
    }

    private bool CanAttack()
    {
        // Check if enough time has passed since the last attack
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return false; // Not ready to attack yet
        }

        // Check if the player is within attack range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D playerCollider in hitPlayer)
        {
            if (playerCollider.CompareTag("Player"))
            {
                return true; // Player is within attack range, so the enemy can attack
            }
        }

        return false; // Player is not within attack range
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
