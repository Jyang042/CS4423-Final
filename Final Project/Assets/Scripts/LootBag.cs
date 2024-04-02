using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public List<GameObject> lootPrefabs = new List<GameObject>();
    [SerializeField] float dropForce = 50f;

    public GameObject GetDroppedItem()
    {
        float totalDropChance = 0f;

        // Calculate total drop chance
        foreach (GameObject lootPrefab in lootPrefabs)
        {
            totalDropChance += lootPrefab.GetComponent<Loot>().dropChance;
        }

        // Generate a random number between 0 and total drop chance
        float randomValue = Random.Range(0f, totalDropChance);

        // Iterate through loot prefabs and determine which one to drop
        foreach (GameObject lootPrefab in lootPrefabs)
        {
            float dropChance = lootPrefab.GetComponent<Loot>().dropChance;

            // If the random value falls within this loot prefab's drop chance range, drop it
            if (randomValue < dropChance)
            {
                return lootPrefab;
            }

            // Subtract this loot prefab's drop chance from randomValue
            randomValue -= dropChance;
        }

        // If no loot prefab is selected, return null
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        GameObject droppedItemPrefab = GetDroppedItem();
        if (droppedItemPrefab != null)
        {
            // Instantiate Object
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);

            // Add Force
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }

    public void RemoveItem(GameObject itemToRemove)
    {
        Destroy(itemToRemove);
    }
}
