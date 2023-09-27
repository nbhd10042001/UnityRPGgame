using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public ListItem listItem;
    public GameObject _prefab;
    public Transform _posContent;
    private List<SlotUpgrade> list_SlotUpgrade = new List<SlotUpgrade>();
    private List<Item> list_itemEquip = new List<Item>();
    private int amountCraft = 20;

    private void OnEnable()
    {
        DestroyAllSlot();
        AddUpgradeSlots();
        UpdateSlotUpgrade();
    }

    private void UpdateSlotUpgrade()
    {
        for (int i = 0; i < list_itemEquip.Count; i++)
            list_SlotUpgrade[i].UpdateSlotUpgrade(list_itemEquip[i], amountCraft);
    }

    private void AddUpgradeSlots()
    {
        for (int i = 0; i < listItem.itemList.Count; i++)
            if (listItem.itemList[i].isEquipment)
                list_itemEquip.Add(listItem.itemList[i]);

        for (int i =0; i < list_itemEquip.Count; i++)
        {
            GameObject gameObj = Instantiate(_prefab, _posContent);
            SlotUpgrade newSlot = gameObj.GetComponent<SlotUpgrade>();
            list_SlotUpgrade.Add(newSlot);
        }
    }

    private void DestroyAllSlot()
    {
        for (int i = 0; i < list_SlotUpgrade.Count; i++)
            list_SlotUpgrade[i].DestroySlot();
        list_SlotUpgrade.Clear();
        list_itemEquip.Clear();
    }
}
