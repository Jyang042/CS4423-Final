using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    public ItemRarity itemRarity; // Instead of string, use enum to represent loot type
    public Sprite ItemSprite;
    public string ItemName;
    public int dropChance;
}
