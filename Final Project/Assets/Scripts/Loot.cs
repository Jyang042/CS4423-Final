using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]

public class Loot : ScriptableObject {
    public Sprite lootSprite;
    public string lootName;
    public int dropChance;
    public int lootValue;
    public AnimatorController animatorController;
}
