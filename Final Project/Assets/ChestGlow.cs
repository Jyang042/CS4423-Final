using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGlow : MonoBehaviour
{
    public Renderer chestRenderer; // Reference to the chest renderer
    public Material glowMaterial; // Reference to the material with glow shader graph
    private Material originalMaterial; // Store the original material of the chest

    private void Start()
    {
        // Store the original material of the chest
        originalMaterial = chestRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player is in range
        {
            // Enable glow effect when the player is in range
            EnableGlow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player exits range
        {
            // Disable glow effect when the player exits range
            DisableGlow();
        }
    }

    private void EnableGlow()
    {
        // Apply glow material to the chest renderer
        chestRenderer.material = glowMaterial;
    }

    private void DisableGlow()
    {
        // Restore the original material of the chest renderer
        chestRenderer.material = originalMaterial;
    }
}