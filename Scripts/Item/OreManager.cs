using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OreManager : MonoBehaviour
{
    public Item m_itemOre;
    private float time;
    public bool isNew;

    private void Update()
    {
        if (time < 1)
            time += Time.deltaTime;
    }

    private void OnEnable()
    {
        time = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (time < 1)
            return;

        if (collision.CompareTag("Player"))
        {
            PlayerStats.Instance.playerInventory.AddItem(m_itemOre.name, m_itemOre.amount);
            PlayerStats.Instance.playerInventory.UpdateCurCapacity();

            if (PlayerStats.Instance.playerInventory.curCapacity > PlayerStats.Instance.playerInventory.maxCapacity)
            {
                PlayerStats.Instance.playerInventory.AddItem(m_itemOre.name, -m_itemOre.amount);
                PlayerStats.Instance.playerInventory.UpdateCurCapacity();

                string text = "Full Inventory!";
                GamePlayManager.Instance.SetTextWarning(text);
                return;
            }

            
            Inventory.Instance.UpdateList();
            if (isNew)
                PlayerStats.Instance.playerCfg.UpdateQuantyQues(gameObject);

            if(PlayerStats.Instance.OnQuantyQuesChange != null)
                PlayerStats.Instance.OnQuantyQuesChange();
            SpawmManager.Instance.ReleaseOre(this, m_itemOre.name);
        }

    }
}
