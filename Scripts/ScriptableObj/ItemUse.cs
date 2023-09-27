using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Item/Potion")]
[System.Serializable]
public class ItemUse : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int price;
    public int regenHP; 
    public float speedRegenMana; 
    public int increDefend; 
    public int increDamage; 
}
