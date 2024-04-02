using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public Material glowMaterial;
    public Color glowColor = Color.yellow;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial; // Original material of the sprite
    private GameObject player; // Reference to the player GameObject
    private ChestController chestController; // Reference to the ChestController script
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject
        originalMaterial = spriteRenderer.material; // Store the original material
        chestController = GetComponentInParent<ChestController>(); // Get reference to ChestController script
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !chestController.isOpen) // If player is in range and chest is not open
        {
            if (Input.GetKeyDown(interactKey)) // Player presses key
            {
                interactAction.Invoke(); // Perform Event
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Player")) 
       {
        isInRange = true;
        Debug.Log("Player is in range");
        EnableGlowEffect();
       }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if(other.CompareTag("Player")) 
       {
        isInRange = false;
        Debug.Log("Player out of range");
        DisableGlowEffect();
       }
    }

    private void EnableGlowEffect()
    {
        if (glowMaterial != null && !chestController.isOpen)
        {
            // Activate glow effect by setting emission color
            glowMaterial.SetColor("_EmissionColor", glowColor);
            spriteRenderer.material = glowMaterial;
        }
    }

    private void DisableGlowEffect()
    {
        if (glowMaterial != null)
        {
            // Revert to original material
            spriteRenderer.material = originalMaterial;
        }
    }
}
