using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager m_Instance;
    public static PlayerManager Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<PlayerManager>();
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

    private string m_curSlot;
    private string m_curSlotDelete;

    public void SetSlot(string slot)
    {
        PlayerPrefs.SetString("slot", slot);
    }

    public void SetSlotDelete(string slot)
    {
        m_curSlotDelete = slot;
    }

    public void LoadDataPlayer()
    {
        m_curSlot = PlayerPrefs.GetString("slot");

        File.Delete(pathSave.Instance.GetPathSave_curPlayer_PlayerCfg());
        File.Delete(pathSave.Instance.GetPathSave_curPlayer_PlayerCfg_meta());
        File.Delete(pathSave.Instance.GetPathSave_curPlayer_Inventory());
        File.Delete(pathSave.Instance.GetPathSave_curPlayer_Inventory_meta());


        File.Copy(pathSave.Instance.GetPathSave_slot_PlayerCfg(m_curSlot), pathSave.Instance.GetPathSave_curPlayer_PlayerCfg());
        File.Copy(pathSave.Instance.GetPathSave_slot_Inventory(m_curSlot), pathSave.Instance.GetPathSave_curPlayer_Inventory());
    }

    public PlayerCfg LoadDataCurPlayer()
    {
        string json = File.ReadAllText(pathSave.Instance.GetPathSave_curPlayer_PlayerCfg());
        PlayerCfg playerCfg = JsonUtility.FromJson<PlayerCfg>(json);
        return playerCfg;
    }

    public PlayerInventory LoadInventoryCurPlayer()
    {
        string json = File.ReadAllText(pathSave.Instance.GetPathSave_curPlayer_Inventory());
        PlayerInventory playerInventory = JsonUtility.FromJson<PlayerInventory>(json);
        return playerInventory;
    }

    public void SaveDataPlayer()
    {
        //save all cfg player to curPlayer if PlayerStats is not null (when play game)
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.saveData();

        m_curSlot = PlayerPrefs.GetString("slot");

        File.Delete(pathSave.Instance.GetPathSave_slot_PlayerCfg(m_curSlot));
        File.Delete(pathSave.Instance.GetPathSave_slot_PlayerCfg_meta(m_curSlot));
        File.Delete(pathSave.Instance.GetPathSave_slot_Inventory(m_curSlot));
        File.Delete(pathSave.Instance.GetPathSave_slot_Inventory_meta(m_curSlot));

        File.Copy(pathSave.Instance.GetPathSave_curPlayer_PlayerCfg(), pathSave.Instance.GetPathSave_slot_PlayerCfg(m_curSlot));
        File.Copy(pathSave.Instance.GetPathSave_curPlayer_Inventory(), pathSave.Instance.GetPathSave_slot_Inventory(m_curSlot));

        //fix new date create
        string json = File.ReadAllText(pathSave.Instance.GetPathSave_slot_PlayerCfg(m_curSlot));
        PlayerCfg playerCfg = JsonUtility.FromJson<PlayerCfg>(json);
        playerCfg.dayCreate = DateTime.Now.ToString();

        json = JsonUtility.ToJson(playerCfg);
        File.WriteAllText(pathSave.Instance.GetPathSave_slot_PlayerCfg(m_curSlot), json);

    }

    public void DeleteDataPlayer()
    {
        File.Delete(pathSave.Instance.GetPathSave_slot_PlayerCfg(m_curSlotDelete));
        File.Delete(pathSave.Instance.GetPathSave_slot_PlayerCfg_meta(m_curSlotDelete));
        File.Delete(pathSave.Instance.GetPathSave_slot_Inventory(m_curSlotDelete));
        File.Delete(pathSave.Instance.GetPathSave_slot_Inventory_meta(m_curSlotDelete));
    }
}
