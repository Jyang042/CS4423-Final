using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;
    public List<GameObject> lootPrefabs = new List<GameObject>();
    [SerializeField] float dropForce = 300f;

    public LayerMask platformLayer; // Reference to the layer mask for platforms

    public void OpenChest()
    {
        if (!isOpen)
        {   
            //Open Chest
            isOpen = true;
            //Debug.Log("Chest Open.");
            animator.SetBool("IsOpen", isOpen);
            //Generate Random loot number
            int totalValue = Random.Range(1, 26); // Generate a random number between 1 and 100
            //Debug.Log("Total Loot Value: " + totalValue);
            // Spawn loot
            SpawnLoot(totalValue);
        }
    }

    private void SpawnLoot(int totalLootValue)
    {
        foreach (GameObject lootPrefab in lootPrefabs)
        {
            Loot lootData = lootPrefab.GetComponent<Loot>(); // Get the Loot component from the prefab
            int lootCount = totalLootValue / lootData.lootValue;
            totalLootValue -= lootCount * lootData.lootValue;

            // Spawn the loot instances
            for (int i = 0; i < lootCount; i++)
            {
                GameObject lootGameObject = Instantiate(lootPrefab, transform.position, Quaternion.identity);
                lootGameObject.name = lootData.lootName;

                // Apply Random Force
                Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            }
        }
    }
}
