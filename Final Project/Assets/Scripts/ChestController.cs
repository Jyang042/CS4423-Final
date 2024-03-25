using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();
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
        // Iterate over each loot object in the loot list
        foreach (Loot loot in lootList)
        {
            int lootCount = totalLootValue / loot.lootValue;
            totalLootValue -= lootCount * loot.lootValue;

            // Spawn the loot instances
            for (int i = 0; i < lootCount; i++)
            {
                // Instantiate the loot object
                GameObject lootGameObject = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity); // Instantiate from the prefab
                lootGameObject.name = loot.lootName;
                lootGameObject.GetComponent<SpriteRenderer>().sprite = loot.lootSprite;

                SpriteRenderer spriteRenderer = lootGameObject.GetComponent<SpriteRenderer>();

                //Apply Animation
                animator = lootGameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = loot.animatorController;

                //Apply Random Force
                Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);

                // Set loot object layer to be the same as platforms
                lootGameObject.layer = LayerMask.NameToLayer("Platforms");
            }
        }
    }
}
