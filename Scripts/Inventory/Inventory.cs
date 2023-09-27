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

    public ListItem listItem;
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

        for (int i = 0; i < listItem.itemList.Count; i++)
            DictItems.Add(listItem.itemList[i].name, playerInventory.GetAmountItem(listItem.itemList[i].name));

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
