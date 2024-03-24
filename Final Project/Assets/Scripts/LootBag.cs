using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();
    [SerializeField] float dropForce = 300f;

    public Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1,101); //1-100
        List<Loot> possibleItems = new List<Loot>();

        foreach(Loot item in lootList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)]; //randomly drop item
            return droppedItem;
        }

        Debug.Log("No Loot Dropped.");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition) {
        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null) {
            Debug.Log("Dropping Loot: " + droppedItem.lootName);
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            // Apply animation controller if loot type is a coin
            if (droppedItem.animatorController != null && droppedItem.lootName == "Coin") {
                Animator animator = lootGameObject.GetComponent<Animator>();
                if (animator != null) {
                    animator.runtimeAnimatorController = droppedItem.animatorController;
                }
            }

            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }

    public void RemoveItem(GameObject itemToRemove) {
        Destroy(itemToRemove);
    }
}
