using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName ="Item", menuName = "Item/baseItem")]

public class Item : ScriptableObject
{
    new public string name = "Default item";
    public string type;
    public int price;
    public Sprite icon = null;
    public int amount;
    public int requiredLevel;
    public bool isEquipment;
    public int slot;
    public int damage;
    public int defend;
    public Item itemCraft;
}
