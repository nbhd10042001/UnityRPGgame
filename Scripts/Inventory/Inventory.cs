using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton
    private static Inventory m_Instance;
    public static Inventory Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<Inventory>();
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }

    #endregion

    public delegate void OnItemChange();
    public OnItemChange onItemChange = delegate { };

    public Dictionary<string, int> DictItems = new Dictionary<string, int>();

    public List<string> List_NameItemOver1 = new List<string>();
    public List<int> List_AmountItemOver1 = new List<int>();

    private void Start()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        PlayerInventory playerInventory = PlayerStats.Instance.playerInventory;
        DictItems.Clear();

        DictItems.Add("OreGreen", playerInventory.OreGreen);
        DictItems.Add("OrePurple", playerInventory.OrePurple);
        DictItems.Add("OreGold", playerInventory.OreGold);
        DictItems.Add("OreOrange", playerInventory.OreOrange);
        DictItems.Add("OreRed", playerInventory.OreRed);
        DictItems.Add("OreCyan", playerInventory.OreCyan);
        DictItems.Add("OreGreenSuper", playerInventory.OreGreenSuper);

        DictItems.Add("SwordEvo2", playerInventory.SwordEvo2);
        DictItems.Add("SwordEvo3", playerInventory.SwordEvo3);
        updateListItemInven();
    }

    public void updateListItemInven()
    {
        List<string> keys = new List<string>(DictItems.Keys);
        List_NameItemOver1.Clear();
        List_AmountItemOver1.Clear();

        for (int i = 0; i < DictItems.Count; i++)
        {
            if (DictItems[keys[i]] != 0)
            {
                List_NameItemOver1.Add(keys[i]);
                List_AmountItemOver1.Add(DictItems[keys[i]]);
            }
        }

        onItemChange.Invoke();
    }
}
