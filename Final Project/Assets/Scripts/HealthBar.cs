using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Animator animator;
    public int maxSegments = 8;
    private int currentSegments;
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        currentSegments = maxSegments;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        // Trigger Damage Accordingly
        if (damage == 1)
        {
            animator.SetTrigger("LightDamage");
        }
        else if (damage == 2)
        {
            animator.SetTrigger("MediumDamage");
        }
        else if (damage == 3)
        {
            animator.SetTrigger("HeavyDamage");
        }
        
        currentSegments -= damage;
        currentSegments = Mathf.Max(currentSegments, 0);
        UpdateHealthBar();
    }

    public void Heal(int healAmount)
    {
        if (currentSegments < maxSegments)
        {
            // Play healing animation
            animator.SetTrigger("Heal");
            currentSegments += healAmount;
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        // Trigger corresponding animation state based on current amount of health
        animator.SetInteger("currentSegments", currentSegments);
        //Debug.Log("Health segments set to: " + currentSegments);
    }

    public void PlayerTakeDamage(int damage)
    {
        // Adjust health directly based on damage received
        TakeDamage(damage);
    }

}
