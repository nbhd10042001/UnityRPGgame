using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUpgrade : MonoBehaviour
{
    [SerializeField] private Image m_imageResult;
    [SerializeField] private Image m_imageCraft;
    [SerializeField] private TextMeshProUGUI m_descrirpt;
    [SerializeField] private TextMeshProUGUI m_txtAmountCraft;
    private Item itemEquipCraft;
    private int amountCraft;

    public void UpdateSlotUpgrade(Item itemCfg, int amount)
    {
        itemEquipCraft = itemCfg;
        amountCraft = amount;
        m_imageResult.sprite = itemEquipCraft.icon;
        m_imageCraft.sprite = itemEquipCraft.itemCraft.icon;

        m_descrirpt.text = $"Name: {itemEquipCraft.name}\n" +
            $"Damage: {itemEquipCraft.damage}\n" +
            $"Defend: {itemEquipCraft.defend}\n" +
            $"Required Level: {itemEquipCraft.requiredLevel}";
        m_txtAmountCraft.text = $"x{amountCraft}";
    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }

    public void btnCraftPressed()
    {
        if (PlayerStats.Instance.playerInventory.GetAmountItem(itemEquipCraft.itemCraft.name) >= amountCraft &&
            PlayerStats.Instance.Level >= itemEquipCraft.requiredLevel)
        {
            print(itemEquipCraft.itemCraft.name);
            PlayerStats.Instance.playerInventory.AddItem(itemEquipCraft.itemCraft.name, -amountCraft);
            PlayerStats.Instance.playerInventory.AddItem(itemEquipCraft.name, 1);
            PlayerStats.Instance.playerInventory.UpdateCurCapacity();
            Inventory.Instance.UpdateList();
        }
        else
            GamePlayManager.Instance.SetTextWarning("Your ore or level not enough to craft this item!");

    }
}
