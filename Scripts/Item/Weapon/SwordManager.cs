using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public Item swordCfg;

    private float time;

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
            PlayerStats.Instance.playerInventory.AddItem(swordCfg.name, swordCfg.amount);
            PlayerStats.Instance.playerInventory.UpdateCurCapacity();

            if (PlayerStats.Instance.playerInventory.curCapacity > PlayerStats.Instance.playerInventory.maxCapacity)
            {
                PlayerStats.Instance.playerInventory.AddItem(swordCfg.name, -swordCfg.amount);
                PlayerStats.Instance.playerInventory.UpdateCurCapacity();

                string text = "Full Inventory!";
                GamePlayManager.Instance.SetTextWarning(text);
                return;
            }

            Inventory.Instance.UpdateList();
            SpawmManager.Instance.ReleaseSword(this, swordCfg.name);
        }
    }
}
