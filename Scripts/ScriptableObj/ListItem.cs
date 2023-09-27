using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "ListItem", menuName = "Item/ListItem")]
public class ListItem : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
