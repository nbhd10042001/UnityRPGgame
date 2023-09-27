using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class pathSave : MonoBehaviour
{
    private static pathSave m_Instance;
    public static pathSave Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<pathSave>();
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


    public string GetPathSave_curPlayer_PlayerCfg()
    {
        return (Application.persistentDataPath + $"/Resources/save/curPlayer/PlayerCfg.json");
    }

    public string GetPathSave_curPlayer_Inventory()
    {
        return (Application.persistentDataPath + $"/Resources/save/curPlayer/Inventory.json");
    }

    public string GetPathSave_slot_PlayerCfg(string slot)
    {
        return (Application.persistentDataPath + $"/Resources/save/{slot}/PlayerCfg.json");
    }

    public string GetPathSave_slot_Inventory(string slot)
    {
        return (Application.persistentDataPath + $"/Resources/save/{slot}/Inventory.json");
    }

    public string GetPathSave_curPlayer_PlayerCfg_meta()
    {
        return (Application.persistentDataPath + $"/Resources/save/curPlayer/PlayerCfg.json.meta");
    }

    public string GetPathSave_curPlayer_Inventory_meta()
    {
        return (Application.persistentDataPath + $"/Resources/save/curPlayer/Inventory.json.meta");
    }

    public string GetPathSave_slot_PlayerCfg_meta(string slot)
    {
        return (Application.persistentDataPath + $"/Resources/save/{slot}/PlayerCfg.json.meta");
    }

    public string GetPathSave_slot_Inventory_meta(string slot)
    {
        return (Application.persistentDataPath + $"/Resources/save/{slot}/Inventory.json.meta");
    }
}
