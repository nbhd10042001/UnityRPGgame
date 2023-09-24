using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject m_InventoryTab;
    [SerializeField] private GameObject m_StatPlayerTab;
    [SerializeField] private TextMeshProUGUI m_txtStatPlayer;
    [SerializeField] private GameObject m_ItemSlotPrefab;
    [SerializeField] private Transform m_inventoryItemTransform;
    [SerializeField] private TextMeshProUGUI m_txtCapacity;

    private List<ItemSlot> itemSlot_List = new List<ItemSlot>();
    public ListItem listItem;

    public List<string> ls_NameItemOver1 = new List<string>();
    public List<int> ls_AmountItemOver1 = new List<int>();
    public List<Item> ls_Item;
    private void OnEnable()
    {
        Inventory.Instance.onItemChange += UpdateListItemInven;
        PlayerStats.Instance.OnCurLevelChange += UpdateTextStatPlayer;
    }

    private void Start()
    {
        UpdateListItemInven();
    }

    public void UpdateTextStatPlayer(int level)
    {
        PlayerCfg playerCfg = PlayerStats.Instance.playerCfg;
        PlayerInventory playerInventory = PlayerStats.Instance.playerInventory;

        m_txtCapacity.text = $"Capacity: {playerInventory.curCapacity}/{playerInventory.maxCapacity}";
        m_txtStatPlayer.text = $"Damage: {playerCfg.damage}\n" +
            $"SpeedAtk: {playerCfg.curSpeedAtk}\n" +
            $"Speed: {playerCfg.curSpeed}\n" +
            $"Speed regen Mana: {playerCfg.speed_regenMana}\n" +
            $"Max HP: {playerCfg.maxHp}\n" +
            $"Max Mana: {playerCfg.maxMana}\n" +
            $"Defend: {playerCfg.defend}\n" +
            $"Max Capacity Inventory: {playerInventory.maxCapacity}\n";
    }

    private void UpdateListItemInven()
    {
        m_txtCapacity.text = $"Capacity: {PlayerStats.Instance.playerInventory.curCapacity}/{PlayerStats.Instance.playerInventory.maxCapacity}";
        DestroySlot();
        ls_Item.Clear();

        ls_NameItemOver1 = Inventory.Instance.List_NameItemOver1;
        ls_AmountItemOver1 = Inventory.Instance.List_AmountItemOver1;

        // sort item have amount > 1
        for (int i = 0; i < ls_NameItemOver1.Count; i++)
        {
            for (int j = 0; j < listItem.itemList.Count; j++)
            {
                if (ls_NameItemOver1[i] == listItem.itemList[j].name)
                    ls_Item.Add(listItem.itemList[j]);
            }
        }
        for (int i = 0; i < ls_NameItemOver1.Count; i++)
            AddItemSlot();

        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < ls_NameItemOver1.Count; i++)
        {
            int curAmountItem = ls_AmountItemOver1[i];
            itemSlot_List[i].AddItem(ls_Item[i], curAmountItem);
        }
    }

    private void AddItemSlot()
    {
        GameObject gameObj = Instantiate(m_ItemSlotPrefab, m_inventoryItemTransform);
        ItemSlot newSlot = gameObj.GetComponent<ItemSlot>();
        itemSlot_List.Add(newSlot);
    }

    private void DestroySlot()
    {
        for (int i = 0; i < itemSlot_List.Count; i++)
            itemSlot_List[i].DestroySlot();
        itemSlot_List.Clear();
    }
    public void BtnOpenInventoryTab()
    {
        m_InventoryTab.SetActive(true);
        m_StatPlayerTab.SetActive(false);
    }

    public void BtnOpenStatPlayerTab()
    {
        m_InventoryTab.SetActive(false);
        m_StatPlayerTab.SetActive(true);
        GamePlayManager.Instance.BtnCloseInfoMaterialTab();
        UpdateTextStatPlayer(0);
    }



    #region Auto UpdateinventoryUI
    //private void UpdateInventoryUI()
    //{
    //    int curItemCount = ls_Item.Count;

    //    if(curItemCount > itemSlot_List.Count)
    //    {
    //        //add more item slots
    //        AddItemSlots(curItemCount);
    //    }

    //    for (int i = 0; i < itemSlot_List.Count; i++)
    //    {
    //        if (i < curItemCount)
    //        {
    //            //update the current item in the slot
    //            itemSlot_List[i].AddItem(ls_Item[i], ls_AmountItemOver1[i]);
    //        }
    //        else
    //        {
    //            itemSlot_List[i].DestroySlot();
    //            itemSlot_List.RemoveAt(i);
    //        }
    //    }
    //}
    #endregion
}
