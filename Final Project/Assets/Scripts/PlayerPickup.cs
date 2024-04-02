using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPickup : MonoBehaviour
{
    public UnityEvent onLootPickup;

    void Start(){
        onLootPickup.AddListener(PrintPickup);
    }

    void PrintPickup()
    {
        Debug.Log("Picked up Loot!");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("Item"))
        {
            // Check if the collided object has the Loot script attached
            Loot loot = other.GetComponent<Loot>();
            if (loot != null)
            {
                onLootPickup.Invoke();
                // Get the loot value from the Loot script
                int lootValue = loot.lootValue;

                // Add the loot value to the gold counter
                GoldCounter.singleton.RegisterLoot(lootValue);

                // Destroy the loot object after collecting
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Heart"))
        {
            Debug.Log("Picked up Health");
            // Heal the player when they pick up a heart
            HealthBar healthBar = FindObjectOfType<HealthBar>();
            if (healthBar != null)
            {
                Debug.Log("Healing");
                healthBar.Heal(1); // Adjust the amount as needed
            }

            // Destroy the heart GameObject after picking it up
            Destroy(other.gameObject);
        }
    }
}
