using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyEquipmentUI : MonoBehaviour
{
    public static BodyEquipmentUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField] private SlotEquipment slot1;
    [SerializeField] private SlotEquipment slot2;
    [SerializeField] private SlotEquipment slot3;
    [SerializeField] private SlotEquipment slot4;

    public ListItem listItem;
    private Item item_equip;

    private void Start()
    {
        UpdateItemEquip();
    }

    public void UpdateItemEquip()
    {
        string nameE = PlayerStats.Instance.playerCfg.slotEquipment_1;
        if (!string.IsNullOrEmpty(nameE))
        {
            CheckListItem(nameE);
            slot1.GetItemEquipment(item_equip);
        }
        else
            slot1.ResetItemEquipment();

        nameE = PlayerStats.Instance.playerCfg.slotEquipment_2;
        if (!string.IsNullOrEmpty(nameE))
        {
            CheckListItem(nameE);
            slot2.GetItemEquipment(item_equip);
        }
        else
            slot2.ResetItemEquipment();

        nameE = PlayerStats.Instance.playerCfg.slotEquipment_3;
        if (!string.IsNullOrEmpty(nameE))
        {
            CheckListItem(nameE);
            slot3.GetItemEquipment(item_equip);
        }
        else
            slot3.ResetItemEquipment();

        nameE = PlayerStats.Instance.playerCfg.slotEquipment_4;
        if (!string.IsNullOrEmpty(nameE))
        {
            CheckListItem(nameE);
            slot4.GetItemEquipment(item_equip);
        }
        else
            slot4.ResetItemEquipment();
    }

    private void CheckListItem (string name)
    {
        for (int i = 0; i < listItem.itemList.Count; i++)
        {
            if (listItem.itemList[i].name == name)
            {
                item_equip = listItem.itemList[i];
                break;
            }
        }  
    }
}
