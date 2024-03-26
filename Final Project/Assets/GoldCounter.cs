using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldCounterText;
    public static GoldCounter singleton;
    private int goldCollected = 0;
    private Player player;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(this.gameObject);
        }
        singleton = this;
        player = FindObjectOfType<Player>(); // Find the Player script in the scene
    }

    void Start()
    {
        
    }

     public void RegisterLoot(Loot loot)
    {
        switch (loot.lootName.ToLower()) // Convert loot name to lowercase for case-insensitive comparison
        {
            case "health":
                // Heal the player by 1
                break;
            case "coin":
            case "gem":
            case "diamond":
                goldCollected += loot.lootValue; // Increment the collected loot based on its value
                goldCounterText.text = goldCollected.ToString(); // Update the UI text with the new loot count
                break;
            // Add cases for other types of loot as needed
        }
    }
}
