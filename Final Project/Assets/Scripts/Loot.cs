using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]

public class Loot : ScriptableObject {
    
    public Sprite lootSprite;
    public string lootName;
    public int dropChance;
    public AnimatorController animatorController;

    public Loot(string lootName, int dropChance){
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
