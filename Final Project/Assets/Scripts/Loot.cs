using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Header ("Stats")]
    [SerializeField] public string lootName;
    [SerializeField] public int lootValue = 1;
    [SerializeField] public float dropChance = 1f;
    
}
